using EFCore.TemporaryTables.PostgreSQL.Operations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace EFCore.TemporaryTables.PostgreSQL;

public static class OptionsBuilderExtensions
{
    public static NpgsqlDbContextOptionsBuilder UseTemporaryTables(
        this NpgsqlDbContextOptionsBuilder relationalOptionsBuilder)
    {
        var relationalDbContextOptionsBuilderInfrastructure =
            relationalOptionsBuilder as IRelationalDbContextOptionsBuilderInfrastructure;

        var optionsBuilder = relationalDbContextOptionsBuilderInfrastructure.OptionsBuilder;
        var dbContextOptionsBuilderInfrastructure = optionsBuilder as 
            IDbContextOptionsBuilderInfrastructure;
        
        var extension = optionsBuilder.Options.FindExtension<TemporaryEntityOptionsExtension<CreateTemporaryEntity, DropTemporaryEntity>>();
        if (!ReferenceEquals(extension, default)) return relationalOptionsBuilder;
        
        dbContextOptionsBuilderInfrastructure.AddOrUpdateExtension(new TemporaryEntityOptionsExtension<CreateTemporaryEntity, DropTemporaryEntity>());

        return relationalOptionsBuilder;
    }
}