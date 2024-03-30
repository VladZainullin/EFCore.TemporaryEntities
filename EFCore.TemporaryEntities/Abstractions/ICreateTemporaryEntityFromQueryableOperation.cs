namespace EFCore.TemporaryTables.Abstractions;

public interface ICreateTemporaryEntityFromQueryableOperation
{
    Task ExecuteAsync<TEntity>(
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default) where TEntity : class;
}