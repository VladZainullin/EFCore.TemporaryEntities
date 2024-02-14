using EFCore.TemporaryTables.PostgreSQL.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace EFCore.TemporaryTables.PostgreSQL;

public static class OptionsBuilderExtensions
{
    public static NpgsqlDbContextOptionsBuilder UseTemporaryTables(
        this NpgsqlDbContextOptionsBuilder sqliteDbContextOptionsBuilder)
    {
        var relationalDbContextOptionsBuilderInfrastructure =
            sqliteDbContextOptionsBuilder as IRelationalDbContextOptionsBuilderInfrastructure;

        var optionsBuilder = relationalDbContextOptionsBuilderInfrastructure.OptionsBuilder;
        var dbContextOptionsBuilderInfrastructure = optionsBuilder as 
            IDbContextOptionsBuilderInfrastructure;
        
        var extension = optionsBuilder.Options.FindExtension<TemporaryTableOptionsExtension<CreateTemporaryTable, DropTemporaryTable>>();
        if (!ReferenceEquals(extension, default)) return sqliteDbContextOptionsBuilder;
        
        dbContextOptionsBuilderInfrastructure.AddOrUpdateExtension(new TemporaryTableOptionsExtension<CreateTemporaryTable, DropTemporaryTable>());

        return sqliteDbContextOptionsBuilder;
    }
}