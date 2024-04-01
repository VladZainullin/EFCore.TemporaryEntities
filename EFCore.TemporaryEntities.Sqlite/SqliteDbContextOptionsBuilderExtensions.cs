using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryEntities.Sqlite;

public static class SqliteDbContextOptionsBuilderExtensions
{
    public static RelationalDbContextOptionsBuilder<T1, T2> UseTemporaryEntities<T1, T2>(
        this RelationalDbContextOptionsBuilder<T1, T2> relationalOptionsBuilder) 
        where T1 : RelationalDbContextOptionsBuilder<T1, T2>
        where T2 : RelationalOptionsExtension, new()
    {
        var relationalDbContextOptionsBuilderInfrastructure =
            relationalOptionsBuilder as IRelationalDbContextOptionsBuilderInfrastructure;

        var optionsBuilder = relationalDbContextOptionsBuilderInfrastructure.OptionsBuilder;
        var dbContextOptionsBuilderInfrastructure = optionsBuilder as
            IDbContextOptionsBuilderInfrastructure;

        var extension = optionsBuilder.Options.FindExtension<SqliteTemporaryEntityOptionsExtension>();
        if (!ReferenceEquals(extension, default)) return relationalOptionsBuilder;

        dbContextOptionsBuilderInfrastructure.AddOrUpdateExtension(new SqliteTemporaryEntityOptionsExtension());

        return relationalOptionsBuilder;
    }
}