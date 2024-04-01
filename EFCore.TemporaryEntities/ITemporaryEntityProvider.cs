namespace EFCore.TemporaryEntities;

public interface ITemporaryEntityProvider
{
    Task CreateAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;
    
    Task DropAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;
}