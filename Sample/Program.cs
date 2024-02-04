using EFCore.TemporaryTables.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Sample;

public static class Program
{
    private static readonly People People = new()
    {
        Id = Guid.NewGuid(),
        Identification = new Identification
        {
            Name = "Vlad",
            Surname = "Zainullin",
            Gender = Gender.Male
        },
        Family = new Family
        {
            QuantityOfChildren = default,
            HasPartner = default
        },
        Work = new Work
        {
            Salary = 1000,
            Address = new Address
            {
                Country = "Russia",
                Region = "Arkhangelsk region",
                City = "Severodvinsk",
                Street = "Arkhangelsk road",
                House = "36"
            }
        },
        Address = new Address
        {
            Country = "Russia",
            Region = "Arkhangelsk region",
            City = "Severodvinsk",
            Street = "Torcheva",
            House = "2"
        }
    };

    public static async Task Main()
    {
        await using var context = new AppDbContext();

        await context.Database.BeginTransactionAsync();

        try
        {
            var temporaryTable = await context.CreateTemporaryTableAsync<People>();

            temporaryTable.Add(People);

            await context.SaveChangesAsync();

            var peoples = await temporaryTable
                .AsNoTracking()
                .Where(p => p.Id == People.Id)
                .SingleAsync();

            await context.DropTemporaryTableAsync<People>();
        }
        catch
        {
            await context.Database.RollbackTransactionAsync();
            throw;
        }
    }
}