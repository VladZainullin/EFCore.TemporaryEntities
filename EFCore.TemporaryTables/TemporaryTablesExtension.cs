using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTablesExtension : IDbContextOptionsExtension
{
    private readonly Action<TemporaryTableOptions> _configureOptions;

    public TemporaryTablesExtension(Action<TemporaryTableOptions> configureOptions)
    {
        _configureOptions = configureOptions;
    }

    public void ApplyServices(IServiceCollection services)
    {
        services.AddScoped(_ =>
        {
            var options = new TemporaryTableOptions();

            _configureOptions(options);

            return options;
        });
        
        services.Decorate<IModelCustomizer, TemporaryTableCustomizer>();
    }

    public void Validate(IDbContextOptions options)
    {
    }

    public DbContextOptionsExtensionInfo Info => new ExtensionInfo(this);

    private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override int GetServiceProviderHashCode()
        {
            return Extension.GetHashCode();
        }

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => other is ExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
        }

        public override bool IsDatabaseProvider => false;
        
        public override string LogFragment => "Using temporary table extension";
    }
}