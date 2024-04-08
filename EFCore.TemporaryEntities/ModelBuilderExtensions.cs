using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryEntities;

public static class ModelBuilderExtensions
{
    public static ModelBuilder TemporaryEntity<TEntity>(
        this ModelBuilder modelBuilder,
        Action<EntityTypeBuilder<TEntity>> configure) where TEntity : class
    {
        var entityTypeBuilder = modelBuilder.Entity<TEntity>();

        entityTypeBuilder.HasAnnotation("Relational:TemporaryEntity", configure);

        configure(entityTypeBuilder);
        entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);

        return modelBuilder;
    }
}