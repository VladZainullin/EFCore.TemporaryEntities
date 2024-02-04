using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder TemporaryEntity<TEntity>(
        this ModelBuilder modelBuilder,
        Action<EntityTypeBuilder<TEntity>> entityTypeBuilder) where TEntity : class
    {
        modelBuilder.Entity(entityTypeBuilder);
        modelBuilder.Entity<TEntity>().Metadata.SetIsTableExcludedFromMigrations(true);

        return modelBuilder;
    }
}