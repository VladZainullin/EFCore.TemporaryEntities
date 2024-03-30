using System.Text;
using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.TemporaryTables.PostgreSQL.Operations;

internal sealed class CreateTemporaryEntity : 
    ICreateTemporaryEntityOperation
{
    private readonly ITemporaryRelationalModelCreator _temporaryRelationalModelCreator;
    private readonly IMigrationsModelDiffer _migrationsModelDiffer;
    private readonly IMigrationsSqlGenerator _migrationsSqlGenerator;
    private readonly ICurrentDbContext _currentDbContext;

    public CreateTemporaryEntity(
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

    public Task ExecuteAsync<TEntity>(CancellationToken cancellationToken = default) 
        where TEntity : class
    {
        var relationalFinalizeModel = _temporaryRelationalModelCreator.Create<TEntity>();
        
        var migrationOperations = _migrationsModelDiffer.GetDifferences(
            default,
            relationalFinalizeModel);
        
        var migrationCommands = _migrationsSqlGenerator.Generate(migrationOperations);

        var stringBuilder = new StringBuilder();

        foreach (var migrationCommand in migrationCommands)
        {
            stringBuilder.Append(migrationCommand.CommandText);
        }
        
        stringBuilder.Replace("CREATE TABLE", "CREATE TEMPORARY TABLE IF NOT EXISTS");

        return _currentDbContext.Context.Database.ExecuteSqlRawAsync(stringBuilder.ToString(), cancellationToken);
    }
}