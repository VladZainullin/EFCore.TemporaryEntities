using System.Text;
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
        var sql = new StringBuilder()
            .Append("CREATE TEMPORARY TABLE IF NOT EXISTS ")
            .Append(_currentDbContext.Context.Model.FindEntityType(typeof(TEntity))?.GetTableName())
            .Append(" AS (")
            .Append(queryable.ToQueryString())
            .Append(')')
            .ToString();

        return _currentDbContext.Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }
}