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
            var table = context.TemporaryTable<Projection>(
                c => c.HasIndex(nameof(Projection.FullName)));

            await table.CreateAsync(cancellationToken);

            var exists = await table.ExistsAsync(cancellationToken);

            await table.DropAsync(cancellationToken);

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