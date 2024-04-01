using EFCore.TemporaryEntities;
using EFCore.TemporaryEntities.Sample;
using Microsoft.EntityFrameworkCore;

namespace Sample;

public static class Program
{
    private static People People => new()
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
            QuantityOfChildren = 0,
            HasPartner = false
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
            for (var i = 0; i < 2; i++)
            {
                var peopleTemporaryTable = await context.CreateTemporaryEntityAsync<People>();

                peopleTemporaryTable.Add(People);

                await context.SaveChangesAsync();

                var peoples = await peopleTemporaryTable
                    .AsNoTracking()
                    .ToListAsync();

                await context.DropTemporaryEntityAsync<People>();
            }
        }
        catch
        {
            await context.Database.RollbackTransactionAsync();
            throw;
        }
    }
}