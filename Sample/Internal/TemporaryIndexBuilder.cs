using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public sealed class TemporaryIndexBuilder : IndexBuilder
{
    private readonly IndexBuilder _indexBuilder;
#pragma warning disable EF1001
    public TemporaryIndexBuilder(IndexBuilder indexBuilder) : base(indexBuilder.Metadata)
#pragma warning restore EF1001
    {
        _indexBuilder = indexBuilder;
    }

    public override IMutableIndex Metadata => _indexBuilder.Metadata;

    public override bool Equals(object? obj)
    {
        return _indexBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _indexBuilder.GetHashCode();
    }

    public override IndexBuilder HasAnnotation(string annotation, object? value)
    {
        return _indexBuilder.HasAnnotation(annotation, value);
    }

    public override string? ToString()
    {
        return _indexBuilder.ToString();
    }

    public override IndexBuilder IsDescending(params bool[] descending)
    {
        return _indexBuilder.IsDescending(descending);
    }

    public override IndexBuilder IsUnique(bool unique = true)
    {
        return _indexBuilder.IsUnique(unique);
    }
}

public sealed class TemporaryIndexBuilder<T> : IndexBuilder<T>
{
    private readonly IndexBuilder<T> _indexBuilder;
#pragma warning disable EF1001
    public TemporaryIndexBuilder(IndexBuilder<T> indexBuilder) : base(indexBuilder.Metadata)
#pragma warning restore EF1001
    {
        _indexBuilder = indexBuilder;
    }
    
    public override IMutableIndex Metadata => _indexBuilder.Metadata;

    public override bool Equals(object? obj)
    {
        return _indexBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _indexBuilder.GetHashCode();
    }

    public override IndexBuilder<T> HasAnnotation(string annotation, object? value)
    {
        return _indexBuilder.HasAnnotation(annotation, value);
    }

    public override string? ToString()
    {
        return _indexBuilder.ToString();
    }

    public override IndexBuilder<T> IsDescending(params bool[] descending)
    {
        return _indexBuilder.IsDescending(descending);
    }

    public override IndexBuilder<T> IsUnique(bool unique = true)
    {
        return _indexBuilder.IsUnique(unique);
    }
}