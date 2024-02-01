using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample;

public static class Program
{
    public static async Task Main()
    {
        var cancellationToken = CancellationToken.None;

        await using var context = new AppDbContext();

        await context.Database.BeginTransactionAsync(cancellationToken);

        var conventionSet = SqliteConventionSetBuilder.Build();
        
        var modelBuilder = new ModelBuilder(conventionSet).Entity(context.EntityTypeBuilder);

        var modelRuntimeInitializer = context.GetService<IModelRuntimeInitializer>();
        var modelDiffer = context.GetService<IMigrationsModelDiffer>();
        var migrationsSqlGenerator = context.GetService<IMigrationsSqlGenerator>();

        var model = modelBuilder.Model.FinalizeModel();

        var runtimeEmptyModelRelational = modelRuntimeInitializer.Initialize(new ModelBuilder().Model.FinalizeModel())
            .GetRelationalModel();

        var runtimeModel = modelRuntimeInitializer.Initialize(model);

        var runtimeModelRelational = runtimeModel.GetRelationalModel();

        var migrationOperations = modelDiffer.GetDifferences(
            runtimeEmptyModelRelational,
            runtimeModelRelational);

        var migrationCommands = migrationsSqlGenerator.Generate(
            migrationOperations);

        var sql = migrationCommands
            .Select(mc => mc.CommandText
                .Replace("CREATE TABLE", "CREATE TEMPORARY TABLE"))
            .Aggregate((s1, s2) => s1 + "\n" + s2);

        await context.Database.ExecuteSqlRawAsync(sql, cancellationToken: cancellationToken);

        var peoples = await context
            .Set<People>()
            .ToListAsync(cancellationToken);

        // try
        // {
        //     var peoples = context.TemporaryTable<People>();
        //     await peoples.CreateAsync(cancellationToken);
        //     await peoples.DropAsync(cancellationToken);
        //     await context.SaveChangesAsync(cancellationToken);
        //
        //     await context.Database.CommitTransactionAsync(cancellationToken);
        // }
        // catch
        // {
        //     await context.Database.RollbackTransactionAsync(cancellationToken);
        //     throw;
        // }
    }

    private static void Swap<T>(ref T old, ref T @new)
    {
        old = @new;
    }
}