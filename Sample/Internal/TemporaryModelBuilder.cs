using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Internal;

public sealed class TemporaryModelBuilder : ModelBuilder
{
    private readonly ModelBuilder _modelBuilder;

    public TemporaryModelBuilder(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public override IMutableModel Model => _modelBuilder.Model;

    public override EntityTypeBuilder<TEntity> Entity<TEntity>()
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_modelBuilder.Entity<TEntity>());
    }

    public override EntityTypeBuilder Entity(Type type)
    {
        return new TemporaryEntityTypeBuilder(_modelBuilder.Entity(type));
    }

    public override EntityTypeBuilder Entity(string name)
    {
        return new TemporaryEntityTypeBuilder(_modelBuilder.Entity(name));
    }

    public override ModelBuilder Entity<TEntity>(Action<EntityTypeBuilder<TEntity>> buildAction)
    {
        _modelBuilder.Entity(buildAction);

        return _modelBuilder;
    }

    public override ModelBuilder Entity(Type type, Action<EntityTypeBuilder> buildAction)
    {
        _modelBuilder.Entity(type, buildAction);

        return _modelBuilder;
    }

    public override ModelBuilder Entity(string name, Action<EntityTypeBuilder> buildAction)
    {
        _modelBuilder.Entity(name, buildAction);

        return _modelBuilder;
    }

    public override ModelBuilder HasAnnotation(string annotation, object? value)
    {
        _modelBuilder.HasAnnotation(annotation, value);

        return _modelBuilder;
    }

    public override OwnedEntityTypeBuilder Owned(Type type)
    {
        return _modelBuilder.Owned(type);
    }

    public override OwnedEntityTypeBuilder<T> Owned<T>()
    {
        return _modelBuilder.Owned<T>();
    }

    public override bool Equals(object? obj)
    {
        return _modelBuilder.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _modelBuilder.GetHashCode();
    }

    public override ModelBuilder Ignore<TEntity>()
    {
        return _modelBuilder.Ignore<TEntity>();
    }

    public override ModelBuilder Ignore(Type type)
    {
        return _modelBuilder.Ignore(type);
    }

    public override ModelBuilder Ignore(string typeName)
    {
        return _modelBuilder.Ignore(typeName);
    }

    public override ModelBuilder ApplyConfiguration<TEntity>(IEntityTypeConfiguration<TEntity> configuration)
    {
        return _modelBuilder.ApplyConfiguration(configuration);
    }

    public override string? ToString()
    {
        return _modelBuilder.ToString();
    }

    public override ModelBuilder SharedTypeEntity<TEntity>(string name, Action<EntityTypeBuilder<TEntity>> buildAction)
    {
        return _modelBuilder.SharedTypeEntity(name, buildAction);
    }

    public override EntityTypeBuilder<TEntity> SharedTypeEntity<TEntity>(string name)
    {
        return new TemporaryEntityTypeBuilder<TEntity>(_modelBuilder.SharedTypeEntity<TEntity>(name));
    }

    public override EntityTypeBuilder SharedTypeEntity(string name, Type type)
    {
        return new TemporaryEntityTypeBuilder(_modelBuilder.SharedTypeEntity(name, type));
    }

    public override ModelBuilder SharedTypeEntity(string name, Type type, Action<EntityTypeBuilder> buildAction)
    {
        return _modelBuilder.SharedTypeEntity(name, type, buildAction);
    }

    public override IModel FinalizeModel()
    {
        return _modelBuilder.FinalizeModel();
    }

    public override ModelBuilder ApplyConfigurationsFromAssembly(Assembly assembly, Func<Type, bool>? predicate = null)
    {
        return _modelBuilder.ApplyConfigurationsFromAssembly(assembly, predicate);
    }

    public override ModelBuilder HasChangeTrackingStrategy(ChangeTrackingStrategy changeTrackingStrategy)
    {
        return _modelBuilder.HasChangeTrackingStrategy(changeTrackingStrategy);
    }

    public override ModelBuilder UsePropertyAccessMode(PropertyAccessMode propertyAccessMode)
    {
        return _modelBuilder.UsePropertyAccessMode(propertyAccessMode);
    }
}