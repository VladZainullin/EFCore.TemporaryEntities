using EFCore.TemporaryTables.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTableCustomizer : IModelCustomizer
{
    private readonly IModelCustomizer _modelCustomizer;

    public TemporaryTableCustomizer(IModelCustomizer modelCustomizer)
    {
        _modelCustomizer = modelCustomizer;
    }

    public void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        _modelCustomizer.Customize(modelBuilder, context);

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        foreach (var type in assembly.GetTypes())
        {
            if (!Attribute.IsDefined(type, typeof(TemporaryTableAttribute))) continue;
            
            var entityTypeBuilder = modelBuilder.Entity(type);

            entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);
        }
    }
}