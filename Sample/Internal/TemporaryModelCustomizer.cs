using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Sample.Internal;

public sealed class TemporaryModelCustomizer : IModelCustomizer
{
    private readonly IModelCustomizer _modelCustomizer;

    public TemporaryModelCustomizer(IModelCustomizer modelCustomizer)
    {
        _modelCustomizer = modelCustomizer;
    }

    public void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        _modelCustomizer.Customize(new TemporaryModelBuilder(modelBuilder), context);
    }
}