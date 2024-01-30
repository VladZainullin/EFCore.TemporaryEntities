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
            var fullNameProjections = context.TemporaryTable<FullNameProjection>();
            await fullNameProjections.CreateAsync(cancellationToken);
            await fullNameProjections.DropAsync(cancellationToken);
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