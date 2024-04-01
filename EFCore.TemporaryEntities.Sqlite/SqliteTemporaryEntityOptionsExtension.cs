using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryEntities.Sqlite;

internal sealed class SqliteTemporaryEntityOptionsExtension : TemporaryEntityOptionsExtension
{
    protected override void ApplyTemporaryTableProvider(IServiceCollection services)
    {
        services.AddScoped<ITemporaryEntityProvider, SqliteTemporaryEntityProvider>();
    }
}