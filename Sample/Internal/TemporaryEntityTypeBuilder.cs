using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public sealed class TemporaryEntityTypeBuilder : EntityTypeBuilder
{
    private readonly EntityTypeBuilder _entityTypeBuilder;

    public TemporaryEntityTypeBuilder(EntityTypeBuilder entityTypeBuilder)
#pragma warning disable EF1001
        : base(entityTypeBuilder.Metadata)
#pragma warning restore EF1001
    {
        _entityTypeBuilder = entityTypeBuilder;
    }

    public override IMutableEntityType Metadata => _entityTypeBuilder.Metadata;

    public override bool Equals(object? obj)
    {
        return _entityTypeBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _entityTypeBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _entityTypeBuilder.ToString();
    }

    public override EntityTypeBuilder Ignore(string propertyName)
    {
        return new TemporaryEntityTypeBuilder(_entityTypeBuilder.Ignore(propertyName));
    }

    public override PropertyBuilder Property(Type propertyType, string propertyName)
    {
        return new TemporaryPropertyBuilder(_entityTypeBuilder.Property(propertyType, propertyName));
    }

    public override PropertyBuilder Property(string propertyName)
    {
        return new TemporaryPropertyBuilder(_entityTypeBuilder.Property(propertyName));
    }

    public override PropertyBuilder<TProperty> Property<TProperty>(string propertyName)
    {
        return new TemporaryPropertyBuilder<TProperty>(_entityTypeBuilder.Property<TProperty>(propertyName));
    }

    public override EntityTypeBuilder HasAnnotation(string annotation, object? value)
    {
        return new TemporaryEntityTypeBuilder(_entityTypeBuilder.HasAnnotation(annotation, value));
    }

    public override EntityTypeBuilder HasChangeTrackingStrategy(ChangeTrackingStrategy changeTrackingStrategy)
    {
        return new TemporaryEntityTypeBuilder(_entityTypeBuilder.HasChangeTrackingStrategy(changeTrackingStrategy));
    }

    public override EntityTypeBuilder UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
    {
        return new TemporaryEntityTypeBuilder(_entityTypeBuilder.UsePropertyAccessMode(propertyAccessMode));
    }

    public override NavigationBuilder Navigation(string navigationName)
    {
        return new TemporaryNavigationBuilder(_entityTypeBuilder.Navigation(navigationName));
    }

    public override DataBuilder HasData(IEnumerable<object> data)
    {
        return new TemporaryDataBuilder(_entityTypeBuilder.HasData(data));
    }

    public override DataBuilder HasData(params object[] data)
    {
        return new TemporaryDataBuilder(_entityTypeBuilder.HasData(data));
    }

    public override DiscriminatorBuilder HasDiscriminator()
    {
        return _entityTypeBuilder.HasDiscriminator();
    }

    public override DiscriminatorBuilder HasDiscriminator(string name, Type type)
    {
        return _entityTypeBuilder.HasDiscriminator(name, type);
    }

    public override DiscriminatorBuilder<TDiscriminator> HasDiscriminator<TDiscriminator>(string name)
    {
        return _entityTypeBuilder.HasDiscriminator<TDiscriminator>(name);
    }

    public override IndexBuilder HasIndex(string[] propertyNames, string name)
    {
        return new TemporaryIndexBuilder(_entityTypeBuilder.HasIndex(propertyNames, name));
    }

    public override IndexBuilder HasIndex(params string[] propertyNames)
    {
        return new TemporaryIndexBuilder(_entityTypeBuilder.HasIndex(propertyNames));
    }

    public override KeyBuilder HasKey(params string[] propertyNames)
    {
        return new TemporaryKeyBuilder(_entityTypeBuilder.HasKey(propertyNames));
    }

    public override CollectionNavigationBuilder HasMany(string navigationName)
    {
        return _entityTypeBuilder.HasMany(navigationName);
    }

    public override CollectionNavigationBuilder HasMany(string relatedTypeName, string? navigationName)
    {
        return _entityTypeBuilder.HasMany(relatedTypeName, navigationName);
    }

    public override CollectionNavigationBuilder HasMany(Type relatedType, string? navigationName = null)
    {
        return _entityTypeBuilder.HasMany(relatedType, navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(string? navigationName)
    {
        return _entityTypeBuilder.HasOne(navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(Type relatedType, string? navigationName = null)
    {
        return _entityTypeBuilder.HasOne(relatedType, navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(string relatedTypeName, string? navigationName)
    {
        return _entityTypeBuilder.HasOne(relatedTypeName, navigationName);
    }

    public override PropertyBuilder IndexerProperty(Type propertyType, string propertyName)
    {
        return new TemporaryPropertyBuilder(_entityTypeBuilder.IndexerProperty(propertyType, propertyName));
    }

    public override PropertyBuilder<TProperty> IndexerProperty<TProperty>(string propertyName)
    {
        return new TemporaryPropertyBuilder<TProperty>(_entityTypeBuilder.IndexerProperty<TProperty>(propertyName));
    }

    public override EntityTypeBuilder OwnsMany(string ownedTypeName, Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return _entityTypeBuilder.OwnsMany(ownedTypeName, ownedType, navigationName, buildAction);
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, string navigationName)
    {
        return _entityTypeBuilder.OwnsMany(ownedTypeName, navigationName);
    }

    public override EntityTypeBuilder OwnsMany(string ownedTypeName, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return _entityTypeBuilder.OwnsMany(ownedTypeName, navigationName, buildAction);
    }

    public override EntityTypeBuilder OwnsMany(Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return _entityTypeBuilder.OwnsMany(ownedType, navigationName, buildAction);
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsMany(ownedTypeName, ownedType, navigationName);
    }

    public override OwnedNavigationBuilder OwnsMany(Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsMany(ownedType, navigationName);
    }

    public override EntityTypeBuilder OwnsOne(string ownedTypeName, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return _entityTypeBuilder.OwnsOne(ownedTypeName, navigationName, buildAction);
    }

    public override EntityTypeBuilder OwnsOne(string ownedTypeName, Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return _entityTypeBuilder.OwnsOne(ownedTypeName, ownedType, navigationName, buildAction);
    }

    public override EntityTypeBuilder OwnsOne(Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return _entityTypeBuilder.OwnsOne(ownedType, navigationName, buildAction);
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, string navigationName)
    {
        return _entityTypeBuilder.OwnsOne(ownedTypeName, navigationName);
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsOne(ownedTypeName, ownedType, navigationName);
    }

    public override OwnedNavigationBuilder OwnsOne(Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsOne(ownedType, navigationName);
    }

    public override KeyBuilder HasAlternateKey(params string[] propertyNames)
    {
        return _entityTypeBuilder.HasAlternateKey(propertyNames);
    }

    public override EntityTypeBuilder HasBaseType(Type? entityType)
    {
        return _entityTypeBuilder.HasBaseType(entityType);
    }

    public override EntityTypeBuilder HasBaseType(string? name)
    {
        return _entityTypeBuilder.HasBaseType(name);
    }

    public override EntityTypeBuilder HasNoDiscriminator()
    {
        return _entityTypeBuilder.HasNoDiscriminator();
    }

    public override EntityTypeBuilder HasNoKey()
    {
        return _entityTypeBuilder.HasNoKey();
    }

    public override EntityTypeBuilder HasQueryFilter(LambdaExpression? filter)
    {
        return _entityTypeBuilder.HasQueryFilter(filter);
    }
}

public sealed class TemporaryEntityTypeBuilder<TEntity> : EntityTypeBuilder<TEntity> where TEntity : class
{
    private readonly EntityTypeBuilder<TEntity> _entityTypeBuilder;

    public TemporaryEntityTypeBuilder(EntityTypeBuilder<TEntity> entityTypeBuilder)
#pragma warning disable EF1001
        : base(entityTypeBuilder.Metadata)
#pragma warning restore EF1001
    {
        _entityTypeBuilder = entityTypeBuilder;
    }

    public override IMutableEntityType Metadata => _entityTypeBuilder.Metadata;

    public override bool Equals(object? obj)
    {
        return _entityTypeBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _entityTypeBuilder.GetHashCode();
    }

    public override string? ToString()
    {
        return _entityTypeBuilder.ToString();
    }

    public override EntityTypeBuilder<TEntity> Ignore(string propertyName)
    {
        return _entityTypeBuilder.Ignore(propertyName);
    }

    public override PropertyBuilder Property(Type propertyType, string propertyName)
    {
        return _entityTypeBuilder.Property(propertyType, propertyName);
    }

    public override PropertyBuilder Property(string propertyName)
    {
        return _entityTypeBuilder.Property(propertyName);
    }

    public override PropertyBuilder<TProperty> Property<TProperty>(string propertyName)
    {
        return new TemporaryPropertyBuilder<TProperty>(_entityTypeBuilder.Property<TProperty>(propertyName));
    }

    public override EntityTypeBuilder<TEntity> HasAnnotation(string annotation, object? value)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasAnnotation(annotation, value));
    }

    public override EntityTypeBuilder<TEntity> HasChangeTrackingStrategy(ChangeTrackingStrategy changeTrackingStrategy)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasChangeTrackingStrategy(changeTrackingStrategy));
    }

    public override EntityTypeBuilder<TEntity> UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.UsePropertyAccessMode(propertyAccessMode));
    }

    public override NavigationBuilder Navigation(string navigationName)
    {
        return new TemporaryNavigationBuilder(_entityTypeBuilder.Navigation(navigationName));
    }

    public override DiscriminatorBuilder HasDiscriminator()
    {
        return _entityTypeBuilder.HasDiscriminator();
    }

    public override DiscriminatorBuilder HasDiscriminator(string name, Type type)
    {
        return _entityTypeBuilder.HasDiscriminator(name, type);
    }

    public override DiscriminatorBuilder<TDiscriminator> HasDiscriminator<TDiscriminator>(string name)
    {
        return _entityTypeBuilder.HasDiscriminator<TDiscriminator>(name);
    }

    public override CollectionNavigationBuilder HasMany(string navigationName)
    {
        return _entityTypeBuilder.HasMany(navigationName);
    }

    public override CollectionNavigationBuilder HasMany(string relatedTypeName, string? navigationName)
    {
        return _entityTypeBuilder.HasMany(relatedTypeName, navigationName);
    }

    public override CollectionNavigationBuilder HasMany(Type relatedType, string? navigationName = null)
    {
        return _entityTypeBuilder.HasMany(relatedType, navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(string? navigationName)
    {
        return _entityTypeBuilder.HasOne(navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(Type relatedType, string? navigationName = null)
    {
        return _entityTypeBuilder.HasOne(relatedType, navigationName);
    }

    public override ReferenceNavigationBuilder HasOne(string relatedTypeName, string? navigationName)
    {
        return _entityTypeBuilder.HasOne(relatedTypeName, navigationName);
    }

    public override PropertyBuilder IndexerProperty(Type propertyType, string propertyName)
    {
        return new TemporaryPropertyBuilder(_entityTypeBuilder.IndexerProperty(propertyType, propertyName));
    }

    public override PropertyBuilder<TProperty> IndexerProperty<TProperty>(string propertyName)
    {
        return new TemporaryPropertyBuilder<TProperty>(_entityTypeBuilder.IndexerProperty<TProperty>(propertyName));
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, string navigationName)
    {
        return _entityTypeBuilder.OwnsMany(ownedTypeName, navigationName);
    }

    public override OwnedNavigationBuilder OwnsMany(string ownedTypeName, Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsMany(ownedTypeName, ownedType, navigationName);
    }

    public override OwnedNavigationBuilder OwnsMany(Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsMany(ownedType, navigationName);
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, string navigationName)
    {
        return _entityTypeBuilder.OwnsOne(ownedTypeName, navigationName);
    }

    public override OwnedNavigationBuilder OwnsOne(string ownedTypeName, Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsOne(ownedTypeName, ownedType, navigationName);
    }

    public override OwnedNavigationBuilder OwnsOne(Type ownedType, string navigationName)
    {
        return _entityTypeBuilder.OwnsOne(ownedType, navigationName);
    }

    public override EntityTypeBuilder<TEntity> HasBaseType(Type? entityType)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasBaseType(entityType));
    }

    public override EntityTypeBuilder<TEntity> HasBaseType(string? name)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasBaseType(name));
    }

    public override EntityTypeBuilder<TEntity> HasNoDiscriminator()
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasNoDiscriminator());
    }

    public override EntityTypeBuilder<TEntity> HasNoKey()
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasNoKey());
    }

    public override EntityTypeBuilder<TEntity> HasQueryFilter(LambdaExpression? filter)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasQueryFilter(filter));
    }

    public override PropertyBuilder<TProperty> Property<TProperty>(
        Expression<Func<TEntity, TProperty>> propertyExpression)
    {
        return new TemporaryPropertyBuilder<TProperty>(_entityTypeBuilder.Property(propertyExpression));
    }

    public override EntityTypeBuilder<TEntity> OwnsMany<TRelatedEntity>(string ownedTypeName,
        Expression<Func<TEntity, IEnumerable<TRelatedEntity>?>> navigationExpression,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsMany(ownedTypeName, navigationExpression, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsMany<TRelatedEntity>(
        Expression<Func<TEntity, IEnumerable<TRelatedEntity>?>> navigationExpression,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsMany(navigationExpression, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsMany(string ownedTypeName, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsMany(ownedTypeName, navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsMany(Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsMany(ownedType, navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsMany<TRelatedEntity>(string ownedTypeName, string navigationName,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsMany(ownedTypeName, navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsMany(string ownedTypeName, Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsMany(ownedTypeName, ownedType, navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsMany<TRelatedEntity>(string navigationName,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsMany(navigationName, buildAction));
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsMany<TRelatedEntity>(string ownedTypeName,
        Expression<Func<TEntity, IEnumerable<TRelatedEntity>?>> navigationExpression)
    {
        return _entityTypeBuilder.OwnsMany(ownedTypeName, navigationExpression);
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsMany<TRelatedEntity>(string navigationName)
    {
        return _entityTypeBuilder.OwnsMany<TRelatedEntity>(navigationName);
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsMany<TRelatedEntity>(string ownedTypeName,
        string navigationName)
    {
        return _entityTypeBuilder.OwnsMany<TRelatedEntity>(ownedTypeName, navigationName);
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsMany<TRelatedEntity>(
        Expression<Func<TEntity, IEnumerable<TRelatedEntity>?>> navigationExpression)
    {
        return _entityTypeBuilder.OwnsMany(navigationExpression);
    }

    public override EntityTypeBuilder<TEntity> OwnsOne(Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsOne(ownedType, navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsOne(string ownedTypeName, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsOne(ownedTypeName, navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsOne<TRelatedEntity>(string navigationName,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsOne(navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsOne<TRelatedEntity>(string ownedTypeName, string navigationName,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsOne(ownedTypeName, navigationName, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsOne<TRelatedEntity>(
        Expression<Func<TEntity, TRelatedEntity?>> navigationExpression,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction) where TRelatedEntity : class
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsOne(navigationExpression, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsOne<TRelatedEntity>(string ownedTypeName,
        Expression<Func<TEntity, TRelatedEntity?>> navigationExpression,
        Action<OwnedNavigationBuilder<TEntity, TRelatedEntity>> buildAction) where TRelatedEntity : class
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsOne(ownedTypeName, navigationExpression, buildAction));
    }

    public override EntityTypeBuilder<TEntity> OwnsOne(string ownedTypeName, Type ownedType, string navigationName,
        Action<OwnedNavigationBuilder> buildAction)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.OwnsOne(ownedTypeName, ownedType, navigationName, buildAction));
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsOne<TRelatedEntity>(string ownedTypeName,
        Expression<Func<TEntity, TRelatedEntity?>> navigationExpression) where TRelatedEntity : class
    {
        return _entityTypeBuilder.OwnsOne(ownedTypeName, navigationExpression);
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsOne<TRelatedEntity>(
        Expression<Func<TEntity, TRelatedEntity?>> navigationExpression) where TRelatedEntity : class
    {
        return _entityTypeBuilder.OwnsOne(navigationExpression);
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsOne<TRelatedEntity>(string ownedTypeName,
        string navigationName)
    {
        return _entityTypeBuilder.OwnsOne<TRelatedEntity>(ownedTypeName, navigationName);
    }

    public override OwnedNavigationBuilder<TEntity, TRelatedEntity> OwnsOne<TRelatedEntity>(string navigationName)
    {
        return _entityTypeBuilder.OwnsOne<TRelatedEntity>(navigationName);
    }

    public override EntityTypeBuilder<TEntity> Ignore(Expression<Func<TEntity, object?>> propertyExpression)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.Ignore(propertyExpression));
    }

    public override DiscriminatorBuilder<TDiscriminator> HasDiscriminator<TDiscriminator>(
        Expression<Func<TEntity, TDiscriminator>> propertyExpression)
    {
        return _entityTypeBuilder.HasDiscriminator(propertyExpression);
    }

    public override CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(
        Expression<Func<TEntity, IEnumerable<TRelatedEntity>?>>? navigationExpression = null)
    {
        return _entityTypeBuilder.HasMany(navigationExpression);
    }

    public override CollectionNavigationBuilder<TEntity, TRelatedEntity> HasMany<TRelatedEntity>(string? navigationName)
    {
        return _entityTypeBuilder.HasMany<TRelatedEntity>(navigationName);
    }

    public override ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(string? navigationName)
    {
        return _entityTypeBuilder.HasOne<TRelatedEntity>(navigationName);
    }

    public override ReferenceNavigationBuilder<TEntity, TRelatedEntity> HasOne<TRelatedEntity>(
        Expression<Func<TEntity, TRelatedEntity?>>? navigationExpression = null) where TRelatedEntity : class
    {
        return _entityTypeBuilder.HasOne(navigationExpression);
    }

    public override DataBuilder<TEntity> HasData(params object[] data)
    {
        return new TemporaryDataBuilder<TEntity>(_entityTypeBuilder.HasData(data));
    }

    public override DataBuilder<TEntity> HasData(IEnumerable<TEntity> data)
    {
        return new TemporaryDataBuilder<TEntity>(_entityTypeBuilder.HasData(data));
    }

    public override DataBuilder<TEntity> HasData(params TEntity[] data)
    {
        return new TemporaryDataBuilder<TEntity>(_entityTypeBuilder.HasData(data));
    }

    public override DataBuilder<TEntity> HasData(IEnumerable<object> data)
    {
        return new TemporaryDataBuilder<TEntity>(_entityTypeBuilder.HasData(data));
    }

    public override IndexBuilder<TEntity> HasIndex(Expression<Func<TEntity, object?>> indexExpression)
    {
        return new TemporaryIndexBuilder<TEntity>(_entityTypeBuilder.HasIndex(indexExpression));
    }

    public override IndexBuilder<TEntity> HasIndex(Expression<Func<TEntity, object?>> indexExpression, string name)
    {
        return new TemporaryIndexBuilder<TEntity>(_entityTypeBuilder.HasIndex(indexExpression, name));
    }

    public override IndexBuilder<TEntity> HasIndex(params string[] propertyNames)
    {
        return new TemporaryIndexBuilder<TEntity>(_entityTypeBuilder.HasIndex(propertyNames));
    }

    public override IndexBuilder<TEntity> HasIndex(string[] propertyNames, string name)
    {
        return new TemporaryIndexBuilder<TEntity>(_entityTypeBuilder.HasIndex(propertyNames, name));
    }

    public override EntityTypeBuilder<TEntity> HasBaseType<TBaseType>()
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasBaseType<TBaseType>());
    }

    public override NavigationBuilder<TEntity, TNavigation> Navigation<TNavigation>(
        Expression<Func<TEntity, TNavigation?>> navigationExpression) where TNavigation : class
    {
        return new TemporaryNavigationBuilder<TEntity, TNavigation>(_entityTypeBuilder.Navigation(navigationExpression));
    }

    public override NavigationBuilder<TEntity, TNavigation> Navigation<TNavigation>(
        Expression<Func<TEntity, IEnumerable<TNavigation>?>> navigationExpression)
    {
        return new TemporaryNavigationBuilder<TEntity, TNavigation>(_entityTypeBuilder.Navigation(navigationExpression));
    }

    public override KeyBuilder HasKey(Expression<Func<TEntity, object?>> keyExpression)
    {
        return new TemporaryKeyBuilder(_entityTypeBuilder.HasKey(keyExpression));
    }

    public override KeyBuilder<TEntity> HasKey(params string[] propertyNames)
    {
        return new TemporaryKeyBuilder<TEntity>(_entityTypeBuilder.HasKey(propertyNames));
    }

    public override KeyBuilder<TEntity> HasAlternateKey(params string[] propertyNames)
    {
        return new TemporaryKeyBuilder<TEntity>(_entityTypeBuilder.HasAlternateKey(propertyNames));
    }

    public override KeyBuilder<TEntity> HasAlternateKey(Expression<Func<TEntity, object?>> keyExpression)
    {
        return new TemporaryKeyBuilder<TEntity>(_entityTypeBuilder.HasAlternateKey(keyExpression));
    }

    public override EntityTypeBuilder<TEntity> HasQueryFilter(Expression<Func<TEntity, bool>>? filter)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_entityTypeBuilder.HasQueryFilter(filter));
    }
}