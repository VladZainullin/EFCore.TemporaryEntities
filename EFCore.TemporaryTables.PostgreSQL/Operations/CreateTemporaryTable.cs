using System.Text;
using EFCore.TemporaryTables.Abstractions;
using EFCore.TemporaryTables.PostgreSQL.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.TemporaryTables.PostgreSQL.Operations;

internal sealed class CreateTemporaryTable : 
    ICreateTemporaryTableOperation
{
    private readonly IConventionSetBuilder _conventionSetBuilder;
    private readonly IModelRuntimeInitializer _modelRuntimeInitializer;
    private readonly IConfigureTemporaryTable _addTemporaryEntityConfiguration;
    private readonly IMigrationsModelDiffer _migrationsModelDiffer;
    private readonly IMigrationsSqlGenerator _migrationsSqlGenerator;
    private readonly ICurrentDbContext _currentDbContext;

    public CreateTemporaryTable(
        IConventionSetBuilder conventionSetBuilder,
        IModelRuntimeInitializer modelRuntimeInitializer,
        IConfigureTemporaryTable addTemporaryEntityConfiguration,
        IMigrationsModelDiffer migrationsModelDiffer,
        IMigrationsSqlGenerator migrationsSqlGenerator,
        ICurrentDbContext currentDbContext)
    {
        _conventionSetBuilder = conventionSetBuilder;
        _modelRuntimeInitializer = modelRuntimeInitializer;
        _addTemporaryEntityConfiguration = addTemporaryEntityConfiguration;
        _migrationsModelDiffer = migrationsModelDiffer;
        _migrationsSqlGenerator = migrationsSqlGenerator;
        _currentDbContext = currentDbContext;
    }

    public Task ExecuteAsync<TEntity>(CancellationToken cancellationToken = default) 
        where TEntity : class
    {
        var conventionSet = _conventionSetBuilder.CreateConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        _addTemporaryEntityConfiguration.Configure<TEntity>(modelBuilder);

        var model = modelBuilder.Model;
        
        var finalizeModel = model.FinalizeModel();

        _modelRuntimeInitializer.Initialize(finalizeModel);
        
        var relationalFinalizeModel = finalizeModel.GetRelationalModel();
        
        var migrationOperations = _migrationsModelDiffer.GetDifferences(
            default,
            relationalFinalizeModel);
        var migrationCommands = _migrationsSqlGenerator.Generate(migrationOperations);

        var stringBuilder = new StringBuilder();

        foreach (var migrationCommand in migrationCommands) stringBuilder.Append(migrationCommand.CommandText);
        
        stringBuilder.Replace("CREATE TABLE", "CREATE TEMPORARY TABLE IF NOT EXISTS");

        return _currentDbContext.Context.Database.ExecuteSqlRawAsync(stringBuilder.ToString(), cancellationToken);
    }

    public void Execute<TEntity>() 
        where TEntity : class
    {
        var conventionSet = _conventionSetBuilder.CreateConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        _addTemporaryEntityConfiguration.Configure<TEntity>(modelBuilder);

        var model = modelBuilder.Model;

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsOwned()) continue;

            if (entityType.ClrType == typeof(TEntity)) continue;

            entityType.SetIsTableExcludedFromMigrations(true);
        }
        
        var finalizeModel = model.FinalizeModel();

        _modelRuntimeInitializer.Initialize(finalizeModel);
        
        var relationalFinalizeModel = finalizeModel.GetRelationalModel();
        
        var migrationOperations = _migrationsModelDiffer.GetDifferences(
            default,
            relationalFinalizeModel);
        var migrationCommands = _migrationsSqlGenerator.Generate(migrationOperations);

        var stringBuilder = new StringBuilder();

        foreach (var migrationCommand in migrationCommands) stringBuilder.Append(migrationCommand.CommandText);
        
        stringBuilder.Replace("CREATE TABLE", "CREATE TEMPORARY TABLE");

        _currentDbContext.Context.Database.ExecuteSqlRaw(stringBuilder.ToString());
    }
}