using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public class TemporaryDiscriminatorBuilder : DiscriminatorBuilder
{
    private readonly DiscriminatorBuilder _discriminatorBuilder;
#pragma warning disable EF1001
    public TemporaryDiscriminatorBuilder(DiscriminatorBuilder discriminatorBuilder, IMutableEntityType mutableEntityType) : base(entityType: mutableEntityType)
#pragma warning restore EF1001
    {
        _discriminatorBuilder = discriminatorBuilder;
    }

    public override bool Equals(object? obj)
    {
        return _discriminatorBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _discriminatorBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _discriminatorBuilder.ToString();
    }

    public override DiscriminatorBuilder HasValue<TEntity>(object? value)
    {
        return _discriminatorBuilder.HasValue<TEntity>(value);
    }

    public override DiscriminatorBuilder HasValue(string entityTypeName, object? value)
    {
        return _discriminatorBuilder.HasValue(entityTypeName, value);
    }

    public override DiscriminatorBuilder HasValue(Type entityType, object? value)
    {
        return _discriminatorBuilder.HasValue(entityType, value);
    }

    public override DiscriminatorBuilder HasValue(object? value)
    {
        return _discriminatorBuilder.HasValue(value);
    }

    public override DiscriminatorBuilder IsComplete(bool complete = true)
    {
        return _discriminatorBuilder.IsComplete(complete);
    }
}