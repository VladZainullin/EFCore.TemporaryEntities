using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryEntities;

public class TemporaryEntityOptionsExtension : IDbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;

    public void ApplyServices(IServiceCollection services)
    {
        ApplyTemporaryTableProvider(services);
    }

    public void Validate(IDbContextOptions options)
    {
    }

    public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

    protected virtual void ApplyTemporaryTableProvider(IServiceCollection services)
    {
        throw new NotSupportedException();
    }

    private sealed class ExtensionInfo(IDbContextOptionsExtension extension) :
        DbContextOptionsExtensionInfo(extension)
    {
        private string? _logFragment;

        public override bool IsDatabaseProvider => default;
        public override string LogFragment => _logFragment ??= "using temporary entities";

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
    }
}