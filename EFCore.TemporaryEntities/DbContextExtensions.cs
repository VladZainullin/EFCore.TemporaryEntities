using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryEntities;

public static class DbContextExtensions
{
    public static async Task<DbSet<TEntity>> CreateTemporaryEntityAsync<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        await context.GetService<ITemporaryEntityProvider>().CreateAsync<TEntity>(cancellationToken);

        return context.Set<TEntity>();
    }

    public static Task DropTemporaryEntityAsync<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        return context.GetService<ITemporaryEntityProvider>().DropAsync<TEntity>(cancellationToken);
    }
}