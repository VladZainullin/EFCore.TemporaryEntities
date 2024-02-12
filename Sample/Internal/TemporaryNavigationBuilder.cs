using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public sealed class TemporaryNavigationBuilder : NavigationBuilder
{
    private readonly NavigationBuilder _navigationBuilder;
#pragma warning disable EF1001
    public TemporaryNavigationBuilder(NavigationBuilder navigationBuilder) : base(navigationBuilder.Metadata)
#pragma warning restore EF1001
    {
        _navigationBuilder = navigationBuilder;
    }

    public override bool Equals(object? obj)
    {
        return _navigationBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _navigationBuilder.GetHashCode();
    }

    public override IMutableNavigationBase Metadata => _navigationBuilder.Metadata;

    public override string? ToString()
    {
        return _navigationBuilder.ToString();
    }

    public override NavigationBuilder HasAnnotation(string annotation, object? value)
    {
        return _navigationBuilder.HasAnnotation(annotation, value);
    }

    public override NavigationBuilder UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
    {
        return _navigationBuilder.UsePropertyAccessMode(propertyAccessMode);
    }

    public override NavigationBuilder HasField(string? fieldName)
    {
        return _navigationBuilder.HasField(fieldName);
    }

    public override NavigationBuilder IsRequired(bool required = true)
    {
        return _navigationBuilder.IsRequired(required);
    }

    public override NavigationBuilder AutoInclude(bool autoInclude = true)
    {
        return _navigationBuilder.AutoInclude(autoInclude);
    }
}

public sealed class TemporaryNavigationBuilder<TSource, TTarget> : NavigationBuilder<TSource, TTarget> 
    where TTarget : class
    where TSource : class
{
    private readonly NavigationBuilder<TSource, TTarget>  _navigationBuilder;
#pragma warning disable EF1001
    public TemporaryNavigationBuilder(NavigationBuilder<TSource, TTarget>  navigationBuilder) : base(navigationBuilder.Metadata)
#pragma warning restore EF1001
    {
        _navigationBuilder = navigationBuilder;
    }

    public override bool Equals(object? obj)
    {
        return _navigationBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _navigationBuilder.GetHashCode();
    }

    public override IMutableNavigationBase Metadata => _navigationBuilder.Metadata;

    public override string? ToString()
    {
        return _navigationBuilder.ToString();
    }

    public override NavigationBuilder<TSource, TTarget>  HasAnnotation(string annotation, object? value)
    {
        return _navigationBuilder.HasAnnotation(annotation, value);
    }

    public override NavigationBuilder<TSource, TTarget>  UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
    {
        return _navigationBuilder.UsePropertyAccessMode(propertyAccessMode);
    }

    public override NavigationBuilder<TSource, TTarget>  HasField(string? fieldName)
    {
        return _navigationBuilder.HasField(fieldName);
    }

    public override NavigationBuilder<TSource, TTarget>  IsRequired(bool required = true)
    {
        return _navigationBuilder.IsRequired(required);
    }

    public override NavigationBuilder<TSource, TTarget>  AutoInclude(bool autoInclude = true)
    {
        return _navigationBuilder.AutoInclude(autoInclude);
    }
}