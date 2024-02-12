using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Sqlite;

public interface IConfigureTemporaryTable
{
    public void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class;
}