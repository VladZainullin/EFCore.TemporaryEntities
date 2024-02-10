using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTableConfiguration
{
    private readonly Dictionary<Type, MulticastDelegate> _configurations = new();

    public void Add<TEntity>(Action<EntityTypeBuilder<TEntity>> configure) where TEntity : class
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