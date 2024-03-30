using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.Extensions;

public static class DbContextExtensions
{
    public static async Task<DbSet<TEntity>> CreateTemporaryTableAsync<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await context.GetService<ICreateTemporaryTableOperation>().ExecuteAsync<TEntity>(cancellationToken);

        return context.Set<TEntity>();
    }
    
    public static async Task<DbSet<TEntity>> CreateTemporaryTableAsync<TEntity>(
        this DbContext context,
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await context.GetService<ICreateTemporaryTableFromQueryableOperation>().ExecuteAsync(queryable, cancellationToken);

        return context.Set<TEntity>();
    }

    public static Task DropTemporaryTableAsync<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return context.GetService<IDropTemporaryTableOperation>().ExecuteAsync<TEntity>(cancellationToken);
    }
}