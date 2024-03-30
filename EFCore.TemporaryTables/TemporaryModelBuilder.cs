using System.Reflection;
using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryModelBuilder : ModelBuilder
{
    private readonly ITemporaryTableConfigurator _configurator;
    private readonly ModelBuilder _modelBuilder;

    public TemporaryModelBuilder(ModelBuilder modelBuilder, ITemporaryTableConfigurator configurator)
    {
        _modelBuilder = modelBuilder;
        _configurator = configurator;
    }

    public override IMutableModel Model => _modelBuilder.Model;

    public override EntityTypeBuilder<TEntity> Entity<TEntity>()
    {
        return _modelBuilder.Entity<TEntity>();
    }

    public override EntityTypeBuilder Entity(Type type)
    {
        return _modelBuilder.Entity(type);
    }

    public override EntityTypeBuilder Entity(string name)
    {
        return _modelBuilder.Entity(name);
    }

    public override ModelBuilder Entity<TEntity>(Action<EntityTypeBuilder<TEntity>> buildAction)
    {
        return _modelBuilder.Entity(buildAction);
    }

    public override ModelBuilder Entity(Type type, Action<EntityTypeBuilder> buildAction)
    {
        return _modelBuilder.Entity(type, buildAction);
    }

    public override ModelBuilder Entity(string name, Action<EntityTypeBuilder> buildAction)
    {
        return _modelBuilder.Entity(name, buildAction);
    }

    public override ModelBuilder HasAnnotation(string annotation, object? value)
    {
        return _modelBuilder.HasAnnotation(annotation, value);
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
        _configurator.Add<TEntity>(configuration.Configure);

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
        return _modelBuilder.SharedTypeEntity<TEntity>(name);
    }

    public override EntityTypeBuilder SharedTypeEntity(string name, Type type)
    {
        return _modelBuilder.SharedTypeEntity(name, type);
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
        var applyEntityConfigurationMethod = typeof(TemporaryModelBuilder)
            .GetMethods()
            .Single(
                e => e is { Name: nameof(ApplyConfiguration), ContainsGenericParameters: true }
                     && e.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition()
                     == typeof(IEntityTypeConfiguration<>));

        var types = assembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsGenericTypeDefinition: false })
            .OrderBy(t => t.FullName);

        foreach (var type in types)
        {
            if (type.GetConstructor(Type.EmptyTypes) == default || (!predicate?.Invoke(type) ?? default)) continue;

            foreach (var @interface in type.GetInterfaces())
            {
                if (!@interface.IsGenericType) continue;
                if (@interface.GetGenericTypeDefinition() != typeof(ITemporaryEntityTypeConfiguration<>)) continue;

                var target = applyEntityConfigurationMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                target.Invoke(this, new[] { Activator.CreateInstance(type) });
            }
        }

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