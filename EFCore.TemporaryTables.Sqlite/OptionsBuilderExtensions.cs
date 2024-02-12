using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.Sqlite;

public static class OptionsBuilderExtensions
{
    public static SqliteDbContextOptionsBuilder UseTemporaryTables(
        this SqliteDbContextOptionsBuilder sqliteDbContextOptionsBuilder)
    {
        var relationalDbContextOptionsBuilderInfrastructure =
            sqliteDbContextOptionsBuilder as IRelationalDbContextOptionsBuilderInfrastructure;

        var optionsBuilder = relationalDbContextOptionsBuilderInfrastructure.OptionsBuilder;
        var dbContextOptionsBuilderInfrastructure = optionsBuilder as
            IDbContextOptionsBuilderInfrastructure;

        var extension = optionsBuilder.Options.FindExtension<TemporaryTableOptionsExtension>();
        if (!ReferenceEquals(extension, default)) return sqliteDbContextOptionsBuilder;

        dbContextOptionsBuilderInfrastructure.AddOrUpdateExtension(new TemporaryTableOptionsExtension());

        return sqliteDbContextOptionsBuilder;
    }
}