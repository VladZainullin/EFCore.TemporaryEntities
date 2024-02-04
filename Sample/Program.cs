using EFCore.TemporaryTables;
using Microsoft.EntityFrameworkCore;

namespace Sample;

public static class Program
{
    public static async Task Main()
    {
        var cancellationToken = CancellationToken.None;

        await using var context = new AppDbContext();

        var table = context.TemporaryTable<People>();
        
        await table.CreateAsync(cancellationToken);

        await table.AddAsync(new People
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
        }, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        var peoples = await table.ToListAsync(cancellationToken);

        await table.DropAsync(cancellationToken);
    }
}