using EFCore.TemporaryTables.Extensions;

namespace Sample;

public static class Program
{
    public static async Task Main()
    {
        var cancellationToken = CancellationToken.None;

        await using var context = new AppDbContext();

        await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var peoples = context.TemporaryTable<People>();
            await peoples.CreateAsync(cancellationToken);
            await peoples.DropAsync(cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await context.Database.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await context.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}