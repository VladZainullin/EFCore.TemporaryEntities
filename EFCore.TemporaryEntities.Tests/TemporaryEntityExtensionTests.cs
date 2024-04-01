using EFCore.TemporaryEntities.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryEntities.Tests;

public sealed class TemporaryEntityExtensionTests
{
    [Fact]
    public void CreateTemporaryEntityOptionsExtensionTest()
    {
        var extension = new TemporaryEntityOptionsExtension();

        Assert.NotNull(extension.Info);
        Assert.Equal("using temporary entities", extension.Info.LogFragment);
        Assert.Equal(default, extension.Info.IsDatabaseProvider);
    }

    [Theory]
    [ClassData(typeof(ProvidersData))]
    public void UseTemporaryEntityOptionsExtensionTest(DbContextOptionsBuilder optionsBuilder)
    {
        var options = optionsBuilder.Options;
        var extension = options.FindExtension<SqliteTemporaryEntityOptionsExtension>();

        Assert.NotNull(extension);
    }
}