using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.PostgreSQL.Abstractions;

public interface IConfigureTemporaryTable
{
    public void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class;
}