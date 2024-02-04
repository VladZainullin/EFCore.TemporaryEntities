using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTableOptionsExtension : IDbContextOptionsExtension
{
    private ExtensionInfo? _info;
    
    public void ApplyServices(IServiceCollection services)
    {
        services
            .AddSingleton<ITemporaryModelBuilderFactory, TemporaryModelBuilderFactory>()
            .AddSingleton<ITemporaryTableSqlGenerator, TemporaryTableSqlGenerator>();
    }

    public void Validate(IDbContextOptions options) {}

    public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

    private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        private readonly Lazy<string> _logFragment = new("using temporary tables");

        public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override int GetServiceProviderHashCode()
        {
            return default;
        }

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
        {
            return other is ExtensionInfo;
        }

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
        }

        public override bool IsDatabaseProvider => default;
        public override string LogFragment => _logFragment.Value;
    }
}