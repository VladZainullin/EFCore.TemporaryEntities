using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables;

public static class ModelBuilderExtensions
{
    public static EntityTypeBuilder<T> ToTemporaryTable<T>(this EntityTypeBuilder<T> builder)
        where T : class
    {
        builder.Metadata.SetIsTableExcludedFromMigrations(true);

        return builder;
    }
}