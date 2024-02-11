using EFCore.TemporaryTables.Abstractions;
using EFCore.TemporaryTables.Providers.InMemory;
using EFCore.TemporaryTables.Providers.PostgreSql;
using EFCore.TemporaryTables.Providers.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTableOptionsExtension : IDbContextOptionsExtension
{
    private ExtensionInfo? _info;

    public void ApplyServices(IServiceCollection services)
    {
        var databaseProvider = services.BuildServiceProvider().GetRequiredService<IDatabaseProvider>();

        services.AddScoped<TemporaryTableConfigurator>();

        var name = databaseProvider.Name;
        switch (name)
        {
            case "Microsoft.EntityFrameworkCore.Sqlite":
                services
                    .AddScoped<ICreateTemporaryTableOperation, SqliteCreateTemporaryTable>()
                    .AddScoped<IDropTemporaryTableOperation, SqliteDropTemporaryTable>();
                break;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                services
                    .AddScoped<ICreateTemporaryTableOperation, NpgsqlCreateTemporaryTable>()
                    .AddScoped<IDropTemporaryTableOperation, NpgsqlDropTemporaryTable>();
                break;
            case "Microsoft.EntityFrameworkCore.InMemory":
                services
                    .AddScoped<ICreateTemporaryTableOperation, InMemoryCreateTemporaryTable>()
                    .AddScoped<IDropTemporaryTableOperation, InMemoryDropTemporaryTable>();
                break;
        }
    }

    public void Validate(IDbContextOptions options)
    {
    }

    public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

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