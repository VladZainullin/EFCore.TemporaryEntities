using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryTablesConfiguration : IEquatable<TemporaryTablesConfiguration>
{
    private static readonly Action<EntityTypeBuilder> Default = etb =>
    {
        etb.Metadata.SetIsTableExcludedFromMigrations(true);
    };

    private readonly ConcurrentDictionary<Type, Action<EntityTypeBuilder>> _configurations = new();

    public bool Equals(TemporaryTablesConfiguration? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || _configurations.Equals(other._configurations);
    }

    public Action<EntityTypeBuilder> GetOrAdd(Type type)
    {
        return _configurations.GetOrAdd(type, Default);
    }

    public void AddOrUpdate(Type type, Action<EntityTypeBuilder> configure)
    {
        _configurations.AddOrUpdate(type, Default, (_, _) => configure);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is TemporaryTablesConfiguration other && Equals(other));
    }

    public override int GetHashCode()
    {
        return _configurations.GetHashCode();
    }
}