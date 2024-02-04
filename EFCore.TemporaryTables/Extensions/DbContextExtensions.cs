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
        var temporaryTableSqlGenerator = context.GetService<ITemporaryTableSqlGenerator>();

        var sql = temporaryTableSqlGenerator.CreateTableSql<TEntity>();

        await context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        return context.Set<TEntity>();
    }
    
    public static DbSet<TEntity> CreateTemporaryTable<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var temporaryTableSqlGenerator = context.GetService<ITemporaryTableSqlGenerator>();

        var sql = temporaryTableSqlGenerator.CreateTableSql<TEntity>();

        context.Database.ExecuteSqlRaw(sql, cancellationToken);

        return context.Set<TEntity>();
    }
    
    public static Task DropTemporaryTableAsync<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var temporaryTableSqlGenerator = context.GetService<ITemporaryTableSqlGenerator>();

        var sql = temporaryTableSqlGenerator.DropTableSql<TEntity>();

        return context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }
    
    public static void DropTemporaryTable<TEntity>(
        this DbContext context,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var temporaryTableSqlGenerator = context.GetService<ITemporaryTableSqlGenerator>();

        var sql = temporaryTableSqlGenerator.DropTableSql<TEntity>();

        context.Database.ExecuteSqlRaw(sql, cancellationToken);
    }
}