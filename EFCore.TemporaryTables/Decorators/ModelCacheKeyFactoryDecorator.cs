using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.Decorators;

internal sealed class ModelCacheKeyFactoryDecorator : IModelCacheKeyFactory
{
    private readonly IModelCacheKeyFactory _modelCacheKeyFactory;

    public ModelCacheKeyFactoryDecorator(IModelCacheKeyFactory modelCacheKeyFactory)
    {
        _modelCacheKeyFactory = modelCacheKeyFactory;
    }
    
    public object Create(DbContext context, bool designTime)
    {
        return HashCode.Combine(_modelCacheKeyFactory.Create(context, designTime), Guid.NewGuid());
    }
}