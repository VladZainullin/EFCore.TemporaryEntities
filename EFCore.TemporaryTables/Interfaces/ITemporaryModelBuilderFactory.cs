using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore.TemporaryTables.Interfaces;

internal interface ITemporaryModelBuilderFactory
{
    IRelationalModel CreateRelationalModelForTemporaryEntity<TEntity>() where TEntity : class;
}