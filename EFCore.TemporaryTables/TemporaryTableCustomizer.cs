using EFCore.TemporaryTables.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTableCustomizer : IModelCustomizer
{
    private readonly IModelCustomizer _modelCustomizer;
    private readonly TemporaryTableOptions _options;

    public TemporaryTableCustomizer(IModelCustomizer modelCustomizer, TemporaryTableOptions options)
    {
        _modelCustomizer = modelCustomizer;
        _options = options;
    }

    public void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        _modelCustomizer.Customize(modelBuilder, context);

        ConfigureTemporaryTables(modelBuilder);
    }

    private void ConfigureTemporaryTables(ModelBuilder modelBuilder)
    {
        var assemblies = _options.Assemblies;

        foreach (var assembly in assemblies)
        foreach (var type in assembly.GetTypes())
        {
            if (!Attribute.IsDefined(type, typeof(TemporaryTableAttribute))) continue;
            
            var entityTypeBuilder = modelBuilder.Entity(type);

            entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);
        }
    }
}