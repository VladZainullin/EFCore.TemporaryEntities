using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTableConfiguration
{
    private readonly Dictionary<Type, object> _configurations = new();

    public void Add<TEntity>(Action<EntityTypeBuilder<TEntity>> configure) where TEntity : class
    {
        _configurations.TryAdd(typeof(TEntity), configure);
    }

    public Action<EntityTypeBuilder<TEntity>> Get<TEntity>() where TEntity : class
    {
        return _configurations[typeof(TEntity)] as Action<EntityTypeBuilder<TEntity>> ??
               throw new InvalidOperationException();
    }
}