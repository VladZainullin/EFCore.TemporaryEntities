using EFCore.TemporaryTables.Extensions;
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
        },
        Religion = new Religion
        {
            Id = Guid.NewGuid(),
            Title = "zavod"
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
                var temporaryTable = await context.CreateTemporaryTableAsync<People>();

                temporaryTable.Add(People);

                await context.SaveChangesAsync();

                var peoples = await temporaryTable
                    .AsNoTracking()
                    .ToListAsync();
                
                await context.DropTemporaryTableAsync<People>();
            }
        }
        catch
        {
            await context.Database.RollbackTransactionAsync();
            throw;
        }
    }
}