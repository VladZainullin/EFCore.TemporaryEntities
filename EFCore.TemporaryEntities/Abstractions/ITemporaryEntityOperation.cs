namespace EFCore.TemporaryTables.Abstractions;

public interface ITemporaryEntityOperation
{
    Task ExecuteAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;
}