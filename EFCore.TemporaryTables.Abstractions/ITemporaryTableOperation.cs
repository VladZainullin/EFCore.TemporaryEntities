namespace EFCore.TemporaryTables.Abstractions;

public interface ITemporaryTableOperation
{
    Task ExecuteAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;
}