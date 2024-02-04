namespace EFCore.TemporaryTables.Interfaces;

internal interface ITemporaryTableSqlGenerator
{
    string CreateTableSql<TEntity>() where TEntity : class;

    string DropTableSql<TEntity>() where TEntity : class;
}