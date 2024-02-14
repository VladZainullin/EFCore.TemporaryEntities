using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Abstractions;

public interface IConfigureTemporaryTable
{
    public void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class;
}