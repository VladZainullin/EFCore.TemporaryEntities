using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public sealed class TemporaryKeyBuilder : KeyBuilder
{
    private readonly KeyBuilder _keyBuilder;
#pragma warning disable EF1001
    public TemporaryKeyBuilder(KeyBuilder keyBuilder) : 
        base(keyBuilder.Metadata)
#pragma warning restore EF1001
    {
        _keyBuilder = keyBuilder;
    }

    public override IMutableKey Metadata => _keyBuilder.Metadata;

    public override bool Equals(object? obj)
    {
        return _keyBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _keyBuilder.GetHashCode();
    }

    public override KeyBuilder HasAnnotation(string annotation, object? value)
    {
        return new TemporaryKeyBuilder(_keyBuilder.HasAnnotation(annotation, value));
    }

    public override string? ToString()
    {
        return _keyBuilder.ToString();
    }
}

public sealed class TemporaryKeyBuilder<TKey> : KeyBuilder<TKey>
{
    private readonly KeyBuilder<TKey> _keyBuilder;

#pragma warning disable EF1001
    public TemporaryKeyBuilder(KeyBuilder<TKey> keyBuilder) : base(keyBuilder.Metadata)
#pragma warning restore EF1001
    {
        _keyBuilder = keyBuilder;
    }
    
    public override IMutableKey Metadata => _keyBuilder.Metadata;

    public override bool Equals(object? obj)
    {
        return _keyBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _keyBuilder.GetHashCode();
    }

    public override KeyBuilder<TKey> HasAnnotation(string annotation, object? value)
    {
        return new TemporaryKeyBuilder<TKey>(_keyBuilder.HasAnnotation(annotation, value));
    }

    public override string? ToString()
    {
        return _keyBuilder.ToString();
    }
}