using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> ToTemporaryTable<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder)
        where TEntity : class
    {
        entityTypeBuilder.Metadata.AddAnnotation(TemporaryTableAnnotationNames.IsTemporaryTable, true);

        return entityTypeBuilder;
    }
}