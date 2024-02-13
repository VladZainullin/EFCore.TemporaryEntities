using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public class TemporaryOwnedNavigationBuilder : OwnedNavigationBuilder
{
    private readonly OwnedNavigationBuilder _ownedNavigationBuilder;
#pragma warning disable EF1001
    public TemporaryOwnedNavigationBuilder(OwnedNavigationBuilder ownedNavigationBuilder) : base(ownedNavigationBuilder.Metadata)
#pragma warning restore EF1001
    {
        _ownedNavigationBuilder = ownedNavigationBuilder;
    }

    public override IMutableForeignKey Metadata => _ownedNavigationBuilder.Metadata;

    public override OwnedNavigationBuilder Ignore(string propertyName)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.Ignore(propertyName));
    }

    public override NavigationBuilder Navigation(string navigationName)
    {
        return new TemporaryNavigationBuilder(_ownedNavigationBuilder.Navigation(navigationName));
    }

    public override PropertyBuilder Property(Type propertyType, string propertyName)
    {
        return new TemporaryPropertyBuilder(_ownedNavigationBuilder.Property(propertyType, propertyName));
    }

    public override PropertyBuilder Property(string propertyName)
    {
        return new TemporaryPropertyBuilder(_ownedNavigationBuilder.Property(propertyName));
    }

    public override PropertyBuilder<TProperty> Property<TProperty>(string propertyName)
    {
        return new TemporaryPropertyBuilder<TProperty>(_ownedNavigationBuilder.Property<TProperty>(propertyName));
    }

    public override OwnedNavigationBuilder HasAnnotation(string annotation, object? value)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.HasAnnotation(annotation, value));
    }

    public override DataBuilder HasData(IEnumerable<object> data)
    {
        return new TemporaryDataBuilder(_ownedNavigationBuilder.HasData(data));
    }

    public override DataBuilder HasData(params object[] data)
    {
        return new TemporaryDataBuilder(_ownedNavigationBuilder.HasData(data));
    }

    public override IndexBuilder HasIndex(params string[] propertyNames)
    {
        return new TemporaryIndexBuilder(_ownedNavigationBuilder.HasIndex(propertyNames));
    }

    public override KeyBuilder HasKey(params string[] propertyNames)
    {
        return new TemporaryKeyBuilder(_ownedNavigationBuilder.HasKey(propertyNames));
    }

    public override ReferenceNavigationBuilder HasOne(string navigationName)
    {
        return _ownedNavigationBuilder.HasOne(navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(string relatedTypeName, string? navigationName)
    {
        return _ownedNavigationBuilder.HasOne(relatedTypeName, navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(Type relatedType, string? navigationName = null)
    {
        return _ownedNavigationBuilder.HasOne(relatedType, navigationName);
    }

    public override PropertyBuilder IndexerProperty(Type propertyType, string propertyName)
    {
        return new TemporaryPropertyBuilder(_ownedNavigationBuilder.IndexerProperty(propertyType, propertyName));
    }

    public override PropertyBuilder<TProperty> IndexerProperty<TProperty>(string propertyName)
    {
        return new TemporaryPropertyBuilder<TProperty>(_ownedNavigationBuilder.IndexerProperty<TProperty>(propertyName));
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, Type ownedType, string navigationName, Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsMany(ownedTypeName, ownedType, navigationName, buildAction));
    }

    public override OwnedNavigationBuilder OwnsMany(Type ownedType, string navigationName, Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsMany(ownedType, navigationName, buildAction));
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, string navigationName, Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsMany(ownedTypeName, navigationName, buildAction));
    }

    public override OwnedNavigationBuilder OwnsMany(Type ownedType, string navigationName)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsMany(ownedType, navigationName));
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, Type ownedType, string navigationName)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsMany(ownedTypeName, ownedType, navigationName));
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, string navigationName)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsMany(ownedTypeName, navigationName));
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, string navigationName)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsOne(ownedTypeName, navigationName));
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, Type ownedType, string navigationName)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsOne(ownedTypeName, ownedType, navigationName));
    }

    public override OwnedNavigationBuilder OwnsOne(Type ownedType, string navigationName)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsOne(ownedType, navigationName));
    }

    public override OwnedNavigationBuilder OwnsOne(Type ownedType, string navigationName, Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsOne(ownedType, navigationName, buildAction));
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, Type ownedType, string navigationName, Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsOne(ownedTypeName, ownedType, navigationName, buildAction));
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, string navigationName, Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.OwnsOne(ownedTypeName, navigationName, buildAction));
    }

    public override OwnershipBuilder WithOwner(string? ownerReference = null)
    {
        return _ownedNavigationBuilder.WithOwner(ownerReference);
    }

    public override IMutableEntityType OwnedEntityType => _ownedNavigationBuilder.OwnedEntityType;

    public override OwnedNavigationBuilder HasChangeTrackingStrategy(ChangeTrackingStrategy changeTrackingStrategy)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.HasChangeTrackingStrategy(changeTrackingStrategy));
    }

    public override OwnedNavigationBuilder UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
    {
        return new TemporaryOwnedNavigationBuilder(_ownedNavigationBuilder.UsePropertyAccessMode(propertyAccessMode));
    }

    public override bool Equals(object? obj)
    {
        return _ownedNavigationBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _ownedNavigationBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _ownedNavigationBuilder.ToString();
    }
}