using EFCore.TemporaryTables.Abstractions;
using EFCore.TemporaryTables.Sqlite.Abstractions;
using EFCore.TemporaryTables.Sqlite.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables.Sqlite;

internal sealed class TemporaryTableOptionsExtension : IDbContextOptionsExtension
{
    public void ApplyServices(IServiceCollection services)
    {
        services.AddScoped<TemporaryTablesConfigurator>();
        services.AddScoped<IAddTemporaryTableConfiguration>(s => s.GetRequiredService<TemporaryTablesConfigurator>());
        services.AddScoped<IConfigureTemporaryTable>(s => s.GetRequiredService<TemporaryTablesConfigurator>());
        services.AddScoped<ICreateTemporaryTableOperation, CreateTemporaryTable>();
        services.AddScoped<IDropTemporaryTableOperation, DropTemporaryTable>();
    }

    public void Validate(IDbContextOptions options)
    {
    }

    public DbContextOptionsExtensionInfo Info => new ExtensionInfo(this);

    private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        private string? _logFragment;

        public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override bool IsDatabaseProvider => default;
        public override string LogFragment => _logFragment ??= "using temporary tables";

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