using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public sealed class TemporaryDataBuilder : DataBuilder
{
    private readonly DataBuilder _dataBuilder;

    public TemporaryDataBuilder(DataBuilder dataBuilder)
    {
        _dataBuilder = dataBuilder;
    }

    public override bool Equals(object? obj)
    {
        return _dataBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _dataBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _dataBuilder.ToString();
    }
}

public sealed class TemporaryDataBuilder<T> : DataBuilder<T>
{
    private readonly DataBuilder<T> _dataBuilder;

    public TemporaryDataBuilder(DataBuilder<T> dataBuilder)
    {
        _dataBuilder = dataBuilder;
    }

    public override bool Equals(object? obj)
    {
        return _dataBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _dataBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _dataBuilder.ToString();
    }
} 