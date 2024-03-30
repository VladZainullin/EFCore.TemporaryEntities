namespace EFCore.TemporaryTables.Abstractions;

public interface ICreateTemporaryTableFromQueryableOperation
{
    Task ExecuteAsync<TEntity>(
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default) where TEntity : class;
}