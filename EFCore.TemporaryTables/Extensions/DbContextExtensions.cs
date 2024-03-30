using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.Extensions;

public static class DbContextExtensions
{
    public static async Task<DbSet<TEntity>> CreateTemporaryEntityAsync<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await context.GetService<ICreateTemporaryTableOperation>().ExecuteAsync<TEntity>(cancellationToken);

        return context.Set<TEntity>();
    }

    public static async Task<DbSet<TEntity>> CreateTemporaryEntityAsync<TEntity>(
        this DbContext context,
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        await context
            .GetService<ICreateTemporaryTableFromQueryableOperation>()
            .ExecuteAsync(queryable, cancellationToken);

        return context.Set<TEntity>();
    }

    public static async Task<DbSet<TEntity>> CreateTemporaryEntityAsync<TContext, TEntity>(
        this TContext context,
        Func<TContext, IQueryable<TEntity>> getQueryable,
        CancellationToken cancellationToken = default)
        where TContext : DbContext
        where TEntity : class

    {
        var queryable = getQueryable(context);
        
        await context
            .GetService<ICreateTemporaryTableFromQueryableOperation>()
            .ExecuteAsync(queryable, cancellationToken);

        return context.Set<TEntity>();
    }

    public static Task DropTemporaryEntityAsync<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        return context
            .GetService<IDropTemporaryTableOperation>()
            .ExecuteAsync<TEntity>(cancellationToken);
    }
}