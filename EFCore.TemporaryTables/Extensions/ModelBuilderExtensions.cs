using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder TemporaryEntity<TEntity>(
        this ModelBuilder modelBuilder,
        Action<EntityTypeBuilder<TEntity>> configure) where TEntity : class
    {
        var mutableEntityType = modelBuilder.Entity<TEntity>().Metadata;

        mutableEntityType.AddAnnotation("TemporaryTable", configure);
        mutableEntityType.SetIsTableExcludedFromMigrations(true);

        configure(modelBuilder.Entity<TEntity>());

        return modelBuilder;
    }
}