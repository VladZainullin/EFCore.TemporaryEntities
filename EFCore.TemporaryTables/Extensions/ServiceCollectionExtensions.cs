using EFCore.TemporaryTables.Abstractions;
using EFCore.TemporaryTables.Providers.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqliteTemporaryTablesServices(this IServiceCollection services, string name)
    {
        services.AddScoped<ICreateTemporaryTableOperation, SqliteCreateTemporaryTable>();
        services.AddScoped<IDropTemporaryTableOperation, SqliteDropTemporaryTable>();
        
        return services;
    }
}