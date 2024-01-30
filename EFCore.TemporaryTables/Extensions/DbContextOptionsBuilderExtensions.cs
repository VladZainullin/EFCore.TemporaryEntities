using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryTables.Extensions;

public static class DbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseTemporaryTables(
        this DbContextOptionsBuilder builder,
        Action<TemporaryTableOptions> options)
    {
        var extension = builder.Options.FindExtension<TemporaryTablesExtension>();

        if (!ReferenceEquals(extension, default)) return builder;

        var dbContextOptionsBuilderInfrastructure = builder as IDbContextOptionsBuilderInfrastructure;
        dbContextOptionsBuilderInfrastructure.AddOrUpdateExtension(new TemporaryTablesExtension(options));

        return builder;
    }
}