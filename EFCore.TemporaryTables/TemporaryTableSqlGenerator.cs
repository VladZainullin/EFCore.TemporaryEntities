using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.TemporaryTables;

public interface ITemporaryTableSqlGenerator
{
    string CreateTableSql<TEntity>() where TEntity : class;

    string DropTableSql<TEntity>() where TEntity : class;
}

internal sealed class TemporaryTableSqlGenerator : ITemporaryTableSqlGenerator
{
    private readonly ITemporaryModelBuilderFactory _temporaryModelBuilderFactory;
    private readonly IMigrationsModelDiffer _migrationsModelDiffer;
    private readonly IMigrationsSqlGenerator _migrationsSqlGenerator;

    public TemporaryTableSqlGenerator(
        ITemporaryModelBuilderFactory temporaryModelBuilderFactory,
        IMigrationsModelDiffer migrationsModelDiffer,
        IMigrationsSqlGenerator migrationsSqlGenerator)
    {
        _temporaryModelBuilderFactory = temporaryModelBuilderFactory;
        _migrationsModelDiffer = migrationsModelDiffer;
        _migrationsSqlGenerator = migrationsSqlGenerator;
    }

    public string CreateTableSql<TEntity>() where TEntity : class
    {
        var relationalFinalizeModel = _temporaryModelBuilderFactory.CreateRelationalModelForTemporaryEntity<TEntity>();

        var sql = CreateSql(target: relationalFinalizeModel);
        return sql;
    }

    public string DropTableSql<TEntity>() where TEntity : class
    {
        var relationalFinalizeModel = _temporaryModelBuilderFactory.CreateRelationalModelForTemporaryEntity<TEntity>();

        var sql = CreateSql(source: relationalFinalizeModel);
        return sql;
    }

    private string CreateSql(IRelationalModel? source = default, IRelationalModel? target = default)
    {
        var migrationOperations = _migrationsModelDiffer.GetDifferences(source, target);
        var migrationCommands = _migrationsSqlGenerator.Generate(migrationOperations);

        var stringBuilder = new StringBuilder();

        foreach (var migrationCommand in migrationCommands)
        {
            stringBuilder.Append(migrationCommand.CommandText);
        }

        stringBuilder.Replace("CREATE TABLE", "CREATE TEMPORARY TABLE IF NOT EXISTS");

        return stringBuilder.ToString();
    }
}