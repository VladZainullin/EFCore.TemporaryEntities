using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.TemporaryEntities.Sqlite;

internal sealed class SqliteTemporaryEntityProvider(
    IConventionSetBuilder conventionSetBuilder,
    ITemporaryEntityConfigurator configureTemporaryEntity,
    IModelRuntimeInitializer modelRuntimeInitializer,
    IMigrationsModelDiffer migrationsModelDiffer,
    IMigrationsSqlGenerator migrationsSqlGenerator,
    ICurrentDbContext currentDbContext) : ITemporaryEntityProvider
{
    public Task CreateAsync<TEntity>(CancellationToken cancellationToken)
        where TEntity : class
    {
        var conventionSet = conventionSetBuilder.CreateConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        configureTemporaryEntity.Configure<TEntity>(modelBuilder);

        var model = modelBuilder.Model;

        var finalizeModel = model.FinalizeModel();

        modelRuntimeInitializer.Initialize(finalizeModel);

        var relationalFinalizeModel = finalizeModel.GetRelationalModel();

        var migrationOperations = migrationsModelDiffer.GetDifferences(
            default,
            relationalFinalizeModel);

        var migrationCommands = migrationsSqlGenerator.Generate(migrationOperations);

        var stringBuilder = new StringBuilder();

        foreach (var migrationCommand in migrationCommands) stringBuilder.Append(migrationCommand.CommandText);

        stringBuilder.Replace("CREATE TABLE", "CREATE TEMPORARY TABLE IF NOT EXISTS");

        return currentDbContext.Context.Database.ExecuteSqlRawAsync(stringBuilder.ToString(), cancellationToken);
    }

    public Task DropAsync<TEntity>(CancellationToken cancellationToken)
        where TEntity : class
    {
        var conventionSet = conventionSetBuilder.CreateConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        configureTemporaryEntity.Configure<TEntity>(modelBuilder);

        var model = modelBuilder.Model;

        var finalizeModel = model.FinalizeModel();

        modelRuntimeInitializer.Initialize(finalizeModel);

        var relationalFinalizeModel = finalizeModel.GetRelationalModel();

        var migrationOperations = migrationsModelDiffer.GetDifferences(
            relationalFinalizeModel,
            default);
        var migrationCommands = migrationsSqlGenerator.Generate(migrationOperations);

        var stringBuilder = new StringBuilder();

        foreach (var migrationCommand in migrationCommands) stringBuilder.Append(migrationCommand.CommandText);

        return currentDbContext.Context.Database.ExecuteSqlRawAsync(stringBuilder.ToString(), cancellationToken);
    }
}