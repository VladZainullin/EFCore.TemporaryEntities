using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public sealed class TemporaryOwnedEntityTypeBuilder : OwnedEntityTypeBuilder
{
    private readonly OwnedEntityTypeBuilder _ownedEntityTypeBuilder;

    public TemporaryOwnedEntityTypeBuilder(OwnedEntityTypeBuilder ownedEntityTypeBuilder)
    {
        _ownedEntityTypeBuilder = ownedEntityTypeBuilder;
    }

    public override bool Equals(object? obj)
    {
        return _ownedEntityTypeBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _ownedEntityTypeBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _ownedEntityTypeBuilder.ToString();
    }
}

public sealed class TemporaryOwnedEntityTypeBuilder<T> : OwnedEntityTypeBuilder<T>
{
    private readonly OwnedEntityTypeBuilder<T> _ownedEntityTypeBuilder;

    public TemporaryOwnedEntityTypeBuilder(OwnedEntityTypeBuilder<T> ownedEntityTypeBuilder)
    {
        _ownedEntityTypeBuilder = ownedEntityTypeBuilder;
    }

    public override bool Equals(object? obj)
    {
        return _ownedEntityTypeBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _ownedEntityTypeBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _ownedEntityTypeBuilder.ToString();
    }
}