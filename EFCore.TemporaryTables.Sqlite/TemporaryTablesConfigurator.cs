using EFCore.TemporaryTables.Abstractions;
using EFCore.TemporaryTables.Sqlite.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables.Sqlite;

internal sealed class TemporaryTablesConfigurator :
    IAddTemporaryTableConfiguration,
    IConfigureTemporaryTable
{
    private readonly IDictionary<Type, MulticastDelegate> _configurations =
        new Dictionary<Type, MulticastDelegate>();

    public void Add<TEntity>(MulticastDelegate configure) where TEntity : class
    {
        if (_configurations.ContainsKey(typeof(TEntity))) return;

        _configurations.Add(typeof(TEntity), configure);
    }

    public void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class
    {
        if (!_configurations.TryGetValue(typeof(TEntity), out var configure)) return;

        if (configure is Action<EntityTypeBuilder<TEntity>> entityTypeConfigure)
            modelBuilder.Entity(entityTypeConfigure);
    }
}