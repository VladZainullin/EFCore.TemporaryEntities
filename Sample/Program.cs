using EFCore.TemporaryTables;
using Microsoft.EntityFrameworkCore;

namespace Sample;

public static class Program
{
    public static async Task Main()
    {
        var cancellationToken = CancellationToken.None;

        await using var context = new AppDbContext();

        await context.Database.BeginTransactionAsync(cancellationToken);

        var table = context.TemporaryTable<People>();

        await table.CreateAsync(cancellationToken);

        var peoples = await table.ToListAsync(cancellationToken);

        await table.DropAsync(cancellationToken);
    }
}