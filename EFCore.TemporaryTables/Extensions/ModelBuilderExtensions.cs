using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder TemporaryEntity<TEntity>(
        this ModelBuilder modelBuilder,
        Action<EntityTypeBuilder<TEntity>> configure) where TEntity : class
    {
        var entityTypeBuilder = modelBuilder.Entity<TEntity>();
        
        configure(entityTypeBuilder);
        
        entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);
        entityTypeBuilder.HasAnnotation("TemporaryTable", configure);

        return modelBuilder;
    }
    
    public static ModelBuilder TemporaryEntity(
        this ModelBuilder modelBuilder,
        Type type,
        Action<EntityTypeBuilder> configure)
    {
        var entityTypeBuilder = modelBuilder.Entity(type);
        
        configure(entityTypeBuilder);
        
        entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);
        entityTypeBuilder.HasAnnotation("TemporaryTable", configure);

        return modelBuilder;
    }
}