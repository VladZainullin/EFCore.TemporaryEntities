using System.Diagnostics;
using EFCore.TemporaryTables.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample;

public static class Program
{
    public static async Task Main()
    {
        var cancellationToken = CancellationToken.None;

        await using var context = new AppDbContext();

        var stopwatch = Stopwatch.StartNew();

        var conventionSetBuilder = context.GetService<IConventionSetBuilder>();
        var conventionSet = conventionSetBuilder.CreateConventionSet();

        var modelDependencies = context.GetService<ModelDependencies>();

        var modelBuilder = new ModelBuilder(conventionSet, modelDependencies);

        var contextConfigure = context.Configure;
        
        contextConfigure(modelBuilder.Entity<People>());

        var model = modelBuilder.Model.FinalizeModel();
        
        var modelRuntimeInitializer = context.GetService<IModelRuntimeInitializer>();
        var runtimeModel = modelRuntimeInitializer.Initialize(model, true);

        var migrationsModelDiffer = context.GetService<IMigrationsModelDiffer>();
        
        var operations = migrationsModelDiffer.GetDifferences(default, runtimeModel.GetRelationalModel());

        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        
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