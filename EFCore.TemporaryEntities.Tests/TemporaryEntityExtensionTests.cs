using EFCore.TemporaryEntities.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
    public void UseTemporaryEntityOptionsExtensionTest<T1, T2>(
        RelationalDbContextOptionsBuilder<T1, T2> relationalOptionsBuilder)
        where T1 : RelationalDbContextOptionsBuilder<T1, T2>
        where T2 : RelationalOptionsExtension, new()
    {
        relationalOptionsBuilder.UseTemporaryEntities();

        var relationalDbContextOptionsBuilderInfrastructure =
            relationalOptionsBuilder as IRelationalDbContextOptionsBuilderInfrastructure;

        var optionsBuilder = relationalDbContextOptionsBuilderInfrastructure.OptionsBuilder;
        var options = optionsBuilder.Options;
        var extension = options.FindExtension<SqliteTemporaryEntityOptionsExtension>();

        Assert.NotNull(extension);
    }
}