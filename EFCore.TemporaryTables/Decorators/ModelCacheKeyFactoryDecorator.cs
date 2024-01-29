using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.Decorators;

internal sealed class ModelCacheKeyFactoryDecorator : IModelCacheKeyFactory
{
    private readonly IModelCacheKeyFactory _modelCacheKeyFactory;
    private readonly TemporaryTablesConfiguration _temporaryTablesConfiguration;

    public ModelCacheKeyFactoryDecorator(
        IModelCacheKeyFactory modelCacheKeyFactory,
        TemporaryTablesConfiguration temporaryTablesConfiguration)
    {
        _modelCacheKeyFactory = modelCacheKeyFactory;
        _temporaryTablesConfiguration = temporaryTablesConfiguration;
    }

    public object Create(DbContext context, bool designTime)
    {
        return HashCode.Combine(_modelCacheKeyFactory.Create(context, designTime), _temporaryTablesConfiguration);
    }
}