using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Abstractions;

internal interface IConfigureTemporaryTable
{
    public void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class;
}