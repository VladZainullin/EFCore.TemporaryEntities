using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore.TemporaryTables.Adapters;

internal sealed class SqliteTemporaryTable<TEntity> : TemporaryTable<TEntity> where TEntity : class
{
    internal SqliteTemporaryTable(DbContext context) : base(context)
    {
    }

    public override async Task CreateAsync(CancellationToken cancellationToken = default)
    {
        var builder = new StringBuilder();

        var designTimeModel = Context.GetService<IDesignTimeModel>();

        var model = designTimeModel.Model;

        var entityType = model.FindEntityType(typeof(TEntity));

        if (ReferenceEquals(entityType, default))
        {
            throw new InvalidOperationException($"Type '{typeof(TEntity)}' not register in DbContext");
        }

        CreateTable(entityType, builder);

        CreateIndexes(entityType, builder);

        var sql = builder.ToString();

        await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }

    private void CreateTable(IEntityType entityType, StringBuilder builder)
    {
        builder
            .Append("create temporary table if not exists \"")
            .Append(entityType.GetTableName())
            .Append("\" (");

        CreateColumns(entityType, builder);

        builder.Remove(builder.Length - 2, 2).Append(");");
    }

    private void CreateColumns(IEntityType entityType, StringBuilder builder)
    {
        foreach (var property in entityType.GetProperties())
        {
            CreateColumn(property, entityType, builder);
        }

        foreach (var navigation in entityType.GetNavigations())
        {
            CreateColumns(navigation.TargetEntityType, builder);
        }
    }

    private void CreateColumn(IProperty property, IEntityType entityType, StringBuilder builder)
    {
        builder
            .Append('"')
            .Append(property.GetColumnName())
            .Append("\" ")
            .Append(property.GetColumnType());
                
        if (property.IsNullable) builder.Append(" null");

        if (property.IsKey())
        {
            var key = property.FindContainingPrimaryKey()!;

            builder
                .Append(" constraint ")
                .Append(key.GetName())
                .Append(" primary key");
                
        }

        var propertyEntityType = DesignTimeModel.Model.FindEntityType(property.ClrType);

        if (!ReferenceEquals(propertyEntityType, default))
        {
            var d = propertyEntityType.IsInOwnershipPath(entityType);
        }
                
        builder.Append(", ");
    }

    private static void CreateIndexes(IEntityType entityType, StringBuilder builder)
    {
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

        builder.Append("select exists(select 1 from sqlite_temp_master where type='table' and name=\'");
        builder.Append(entityType.GetTableName());
        builder.Append("\')");

        var sql = builder.ToString();

        var exists = await Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);

        return exists != default;
    }
}