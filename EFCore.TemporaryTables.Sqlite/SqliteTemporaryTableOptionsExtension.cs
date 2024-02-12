using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables.Sqlite;

internal sealed class SqliteTemporaryTableOptionsExtension : IDbContextOptionsExtension
{
    public void ApplyServices(IServiceCollection services)
    {
        services.AddScoped<IAddTemporaryTableConfiguration, SqliteTemporaryTablesConfigurator>();
        services.AddScoped<IConfigureTemporaryTable, SqliteTemporaryTablesConfigurator>();
        services.AddScoped<ICreateTemporaryTableOperation, SqliteCreateTemporaryTable>();
        services.AddScoped<IDropTemporaryTableOperation, SqliteDropTemporaryTable>();
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