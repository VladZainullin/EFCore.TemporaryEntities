using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ToTemporaryTable<TEntity>(this ModelBuilder builder)
        where TEntity : class
    {
        var entityTypeBuilder = builder.Entity<TEntity>();
        
        entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);

        return builder;
    }
}