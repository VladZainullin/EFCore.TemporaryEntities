using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryEntities.Sqlite;

public static class SqliteDbContextOptionsBuilderExtensions
{
    public static SqliteDbContextOptionsBuilder UseTemporaryEntities(
        this SqliteDbContextOptionsBuilder relationalOptionsBuilder)
    {
        var relationalDbContextOptionsBuilderInfrastructure =
            relationalOptionsBuilder as IRelationalDbContextOptionsBuilderInfrastructure;

        var optionsBuilder = relationalDbContextOptionsBuilderInfrastructure.OptionsBuilder;
        var dbContextOptionsBuilderInfrastructure = optionsBuilder as
            IDbContextOptionsBuilderInfrastructure;

        optionsBuilder.ReplaceService<IAnnotationCodeGenerator, ExcludeTemporaryEntityAnnotationCodeGenerator>();

        var extension = optionsBuilder.Options.FindExtension<SqliteTemporaryEntityOptionsExtension>();
        if (!ReferenceEquals(extension, default)) return relationalOptionsBuilder;

        dbContextOptionsBuilderInfrastructure.AddOrUpdateExtension(new SqliteTemporaryEntityOptionsExtension());

        return relationalOptionsBuilder;
    }
}