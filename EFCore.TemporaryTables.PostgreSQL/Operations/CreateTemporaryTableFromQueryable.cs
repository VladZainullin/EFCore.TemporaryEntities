using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.PostgreSQL.Operations;

internal sealed class CreateTemporaryTableFromQueryable : ICreateTemporaryTableFromQueryableOperation
{
    private readonly ICurrentDbContext _currentDbContext;

    public CreateTemporaryTableFromQueryable(ICurrentDbContext currentDbContext)
    {
        _currentDbContext = currentDbContext;
    }

    public Task ExecuteAsync<TEntity>(
        IQueryable<TEntity> queryable,
        CancellationToken cancellationToken = default) where TEntity : class

    {
        var sql = "CREATE TEMPORARY TABLE IF NOT EXISTS "
                + _currentDbContext.Context.Model.FindEntityType(typeof(TEntity))?.GetTableName()
                + " AS ("
                + queryable.ToQueryString()
                + ")";

        return _currentDbContext.Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }
}