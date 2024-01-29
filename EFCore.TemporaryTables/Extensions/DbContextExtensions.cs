using EFCore.TemporaryTables.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables.Extensions;

public static class DbContextExtensions
{
    public static TemporaryTable<TEntity> TemporaryTable<TEntity>(
        this DbContext context,
        Action<EntityTypeBuilder>? configure = default) where TEntity : class
    {
        if (!ReferenceEquals(configure, default))
        {
            var configurator = context.GetService<TemporaryTablesConfiguration>();

            if (!ReferenceEquals(configurator, default)) configurator.AddOrUpdate(typeof(TEntity), configure);
        }

        return context.Database.ProviderName switch
        {
            "Npgsql.EntityFrameworkCore.PostgreSQL" => new NpgsqlTemporaryTable<TEntity>(context),
            "Microsoft.EntityFrameworkCore.Sqlite" => new SqliteTemporaryTable<TEntity>(context),
            _ => throw new ArgumentOutOfRangeException(
                nameof(context.Database.ProviderName),
                "Your database provider not support")
        };
    }
}