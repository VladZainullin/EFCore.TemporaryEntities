using System.Text;
using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.TemporaryTables.PostgreSQL.Operations;

internal sealed class DropTemporaryTable : IDropTemporaryTableOperation
{
    private readonly ITemporaryRelationalModelCreator _temporaryRelationalModelCreator;
    private readonly IMigrationsModelDiffer _migrationsModelDiffer;
    private readonly IMigrationsSqlGenerator _migrationsSqlGenerator;
    private readonly ICurrentDbContext _currentDbContext;

    public DropTemporaryTable(
        ITemporaryRelationalModelCreator temporaryRelationalModelCreator,
        IMigrationsModelDiffer migrationsModelDiffer,
        IMigrationsSqlGenerator migrationsSqlGenerator,
        ICurrentDbContext currentDbContext)
    {
        _temporaryRelationalModelCreator = temporaryRelationalModelCreator;
        _migrationsModelDiffer = migrationsModelDiffer;
        _migrationsSqlGenerator = migrationsSqlGenerator;
        _currentDbContext = currentDbContext;
    }
    
    public Task ExecuteAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
    {
        var relationalFinalizeModel = _temporaryRelationalModelCreator.Create<TEntity>();
        
        var migrationOperations = _migrationsModelDiffer.GetDifferences(
            relationalFinalizeModel,
            default);
        var migrationCommands = _migrationsSqlGenerator.Generate(migrationOperations);

        var stringBuilder = new StringBuilder();

        foreach (var migrationCommand in migrationCommands)
        {
            stringBuilder.Append(migrationCommand.CommandText);
        }

        return _currentDbContext.Context.Database.ExecuteSqlRawAsync(stringBuilder.ToString(), cancellationToken);
    }
}