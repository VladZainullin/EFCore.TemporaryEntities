using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables;

internal class TemporaryModelCustomizer : IModelCustomizer
{
    private readonly IModelCustomizer _modelCustomizer;

    public TemporaryModelCustomizer(IModelCustomizer modelCustomizer)
    {
        _modelCustomizer = modelCustomizer;
    }

    public void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        var configurator = context.GetService<ITemporaryEntityConfigurator>();

        _modelCustomizer.Customize(new TemporaryModelBuilder(modelBuilder, configurator), context);
    }
}