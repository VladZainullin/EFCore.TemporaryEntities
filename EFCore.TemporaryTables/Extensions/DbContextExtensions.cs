using EFCore.TemporaryTables.Adapters;
using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Extensions;

public static class DbContextExtensions
{
    public static TemporaryTable<TEntity> TemporaryTable<TEntity>(this DbContext context) where TEntity : class
    {
        return context.Database.ProviderName switch
        {
            "Npgsql.EntityFrameworkCore.PostgreSQL" => new NpgsqlTemporaryTable<TEntity>(context),
            "Microsoft.EntityFrameworkCore.Sqlite" => new SqliteTemporaryTable<TEntity>(context),
            _ => throw new ArgumentOutOfRangeException("Your database provider not support")
        };
    }
}