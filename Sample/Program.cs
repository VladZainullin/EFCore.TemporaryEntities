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
            DateOfBirth = DateTime.Parse("24.08.2002"),
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
        var cancellationToken = CancellationToken.None;

        await using var context = new AppDbContext();

        var set = await context.CreateTemporaryTableAsync<People>(cancellationToken);

        set.Add(People);

        await context.SaveChangesAsync(cancellationToken);
        
        var peoples = await set.ToListAsync(cancellationToken);
        
        await context.DropTemporaryTableAsync<People>(cancellationToken);
    }
}