using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFCore.TemporaryEntities.Tests;

public sealed class TemporaryEntityConfiguratorTests
{
    [Fact]
    public void TemporaryEntityAdd()
    {
        var configurator = new TemporaryEntityConfigurator();
        
        configurator.Add<Animal>(builder =>
        {
            builder.OwnsOne<People>(a => a.People);
        });

        var conventionSetBuilder = SqliteConventionSetBuilder.Build();

        var modelBuilder = new ModelBuilder(conventionSetBuilder);
        
        configurator.Configure<Animal>(modelBuilder);

        Assert.NotNull(modelBuilder.Entity<Animal>());
        Assert.NotNull(modelBuilder.Owned<People>());
    }
}

file sealed class Animal
{
    public required int Id { get; init; }

    public required string Title { get; init; }

    public required People People { get; init; }
}

file sealed class People
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}