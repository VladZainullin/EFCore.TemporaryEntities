using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.Extensions;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseTemporaryTables(this DbContextOptionsBuilder builder)
    {
        var extension = builder.Options.FindExtension<TemporaryTablesExtension>();
        
        if (!ReferenceEquals(extension, default)) return builder;
        
        var dbContextOptionsBuilderInfrastructure = builder as IDbContextOptionsBuilderInfrastructure;
        dbContextOptionsBuilderInfrastructure.AddOrUpdateExtension(new TemporaryTablesExtension());

        return builder;
    }
}