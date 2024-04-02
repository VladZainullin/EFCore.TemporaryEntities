using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore.TemporaryEntities.Sqlite;

internal sealed class SqliteTemporaryEntityProvider(
    IConventionSetBuilder conventionSetBuilder,
    IModelRuntimeInitializer modelRuntimeInitializer,
    IMigrationsModelDiffer migrationsModelDiffer,
    IMigrationsSqlGenerator migrationsSqlGenerator,
    ICurrentDbContext currentDbContext) : ITemporaryEntityProvider
{
    public Task CreateAsync<TEntity>(CancellationToken cancellationToken)
        where TEntity : class
    {
        var entityType = currentDbContext.Context.Model.FindEntityType(typeof(TEntity));
        var annotation = entityType?.FindAnnotation("TemporaryEntity");
        var configure = annotation?.Value as Action<EntityTypeBuilder<TEntity>>;
        
        var conventionSet = conventionSetBuilder.CreateConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        configure?.Invoke(modelBuilder.Entity<TEntity>());

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

        var entityType = currentDbContext.Context.Model.FindEntityType(typeof(TEntity));

        var annotation = entityType?.FindAnnotation("TemporaryEntity");

        var configure = annotation?.Value as Action<EntityTypeBuilder<TEntity>>;

        configure?.Invoke(modelBuilder.Entity<TEntity>());

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