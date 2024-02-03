using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample;

public static class Program
{
    public static async Task Main()
    {
        var cancellationToken = CancellationToken.None;

        await using var context = new AppDbContext();

        await context.Database.BeginTransactionAsync(cancellationToken);
        
        var stopwatch = Stopwatch.StartNew();
        
        var modelDiffer = context.GetService<IMigrationsModelDiffer>();
        var migrationsSqlGenerator = context.GetService<IMigrationsSqlGenerator>();

        var designTimeModel = context.GetService<IDesignTimeModel>();
        var relationalModel = designTimeModel.Model.GetRelationalModel();

        var findEntityType = context.Model.FindEntityType(typeof(People));
        if (ReferenceEquals(findEntityType, default))
        {
            throw new Exception();
        }
        
        var temporaryTableRelationalModel = new TemporaryTableRelationalModel(
            relationalModel,
            findEntityType);
        
        var migrationOperations = modelDiffer.GetDifferences(
            default,
            temporaryTableRelationalModel);
        
        var migrationCommands = migrationsSqlGenerator.Generate(
            migrationOperations);

        var sql = migrationCommands
            .Select(mc => mc.CommandText
                .Replace("CREATE TABLE", "CREATE TEMPORARY TABLE"))
            .Aggregate((s1, s2) => s1 + "\n" + s2);

        await context.Database.ExecuteSqlRawAsync(sql, cancellationToken: cancellationToken);
        
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        var peoples = await context
            .Set<People>()
            .ToListAsync(cancellationToken);
    }
}