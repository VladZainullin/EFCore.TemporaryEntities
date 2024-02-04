using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Extensions;

public static class DbContextExtensions
{
    public static TemporaryTable<TEntity> TemporaryTable<TEntity>(this DbContext context) where TEntity : class
    {
        return new TemporaryTable<TEntity>(context);
    }
}