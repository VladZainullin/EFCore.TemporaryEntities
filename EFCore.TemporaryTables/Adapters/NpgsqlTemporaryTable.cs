using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Adapters;

internal sealed class NpgsqlTemporaryTable<TEntity> : TemporaryTable<TEntity> where TEntity : class
{
    public NpgsqlTemporaryTable(DbContext context) : base(context)
    {
    }

    public override async Task CreateAsync(CancellationToken cancellationToken = default)
    {
        var builder = new StringBuilder();

        var entityType = Context.Model.FindEntityType(typeof(TEntity));

        builder
            .Append("create temporary table \"")
            .Append(entityType.GetTableName())
            .Append("\" (");

        foreach (var property in entityType.GetProperties())
        {
            builder
                .Append('"')
                .Append(property.GetColumnName())
                .Append("\" ")
                .Append(property.GetColumnType());

            if (property.IsNullable) builder.Append(" null");

            builder.Append(", ");
        }

        builder.Remove(builder.Length - 2, 2).Append(");");

        foreach (var index in entityType.GetIndexes())
        {
            builder
                .AppendLine()
                .Append("create");

            if (index.IsUnique) builder.Append(" unique");

            builder
                .Append(" index ")
                .Append(index.GetDatabaseName());

            builder
                .Append(" on \"")
                .Append(entityType.GetTableName());

            builder.Append("\" (");

            for (var i = 0; i < index.Properties.Count; i++)
            {
                builder.Append('"').Append(index.Properties[i].GetColumnName()).Append('"');

                if (!ReferenceEquals(index.IsDescending, default)
                    && index.IsDescending.Count != default
                    && index.IsDescending[i])
                    builder.Append(" desc");

                if (i != index.Properties.Count - 1) builder.Append(", ");
            }

            builder.Append(");");
        }

        var sql = builder.ToString();

        await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }

    public override async Task DropAsync(CancellationToken cancellationToken = default)
    {
        var entityType = Context.Set<TEntity>().EntityType;

        var builder = new StringBuilder();

        builder
            .Append("drop table if exists \"")
            .Append(entityType.GetTableName())
            .Append('\"');

        var sql = builder.ToString();

        await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }

    public override async Task<bool> ExistsAsync(CancellationToken cancellationToken = default)
    {
        var entityType = Context.Set<TEntity>().EntityType;

        var builder = new StringBuilder();

        builder.Append(
            "SELECT EXISTS(SELECT 1 FROM pg_tables t inner join pg_namespace n on t.schemaname = n.nspname WHERE n.oid = pg_my_temp_schema() and t.tablename = '");
        builder.Append(entityType.GetTableName());
        builder.Append("\');");

        var sql = builder.ToString();

        var exists = await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        return exists != default;
    }
}