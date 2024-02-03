using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sample;

public class TemporaryTableRelationalModel : IRelationalModel
{
    private readonly IRelationalModel _relationalModel;
    private readonly IEntityType _entityType;

    public TemporaryTableRelationalModel(IRelationalModel relationalModel, IEntityType entityType)
    {
        _relationalModel = relationalModel;
        _entityType = entityType;
    }
    
    public IAnnotation? FindAnnotation(string name)
    {
        return default;
    }

    public IEnumerable<IAnnotation> GetAnnotations()
    {
        return Enumerable.Empty<IAnnotation>();
    }

    public object? this[string name] => _relationalModel[name];

    public IAnnotation? FindRuntimeAnnotation(string name)
    {
        return default;
    }

    public IEnumerable<IAnnotation> GetRuntimeAnnotations()
    {
        return Enumerable.Empty<IAnnotation>();
    }

    public IAnnotation AddRuntimeAnnotation(string name, object? value)
    {
        return _relationalModel.AddRuntimeAnnotation(name, value);
    }

    public IAnnotation SetRuntimeAnnotation(string name, object? value)
    {
        return _relationalModel.AddRuntimeAnnotation(name, value);
    }

    public IAnnotation? RemoveRuntimeAnnotation(string name)
    {
        return default;
    }

    public TValue GetOrAddRuntimeAnnotationValue<TValue, TArg>(string name, Func<TArg?, TValue> valueFactory, TArg? factoryArgument)
    {
        return _relationalModel.GetOrAddRuntimeAnnotationValue(name, valueFactory, factoryArgument);
    }

    public ITable? FindTable(string name, string? schema)
    {
        return default;
    }

    public IView? FindView(string name, string? schema)
    {
        return default;
    }

    public ISqlQuery? FindQuery(string name)
    {
        return default;
    }

    public IStoreFunction? FindFunction(string name, string? schema, IReadOnlyList<string> parameters)
    {
        return default;
    }

    public IStoreStoredProcedure? FindStoredProcedure(string name, string? schema)
    {
        return _relationalModel.FindStoredProcedure(name, schema);
    }

    public IModel Model => _relationalModel.Model;

    public IEnumerable<ITable> Tables
    {
        get
        {
            return _relationalModel.Tables
                .Where(t => t.Name == _entityType.GetTableName())
                .Select(t => new IncludeInMigrationTableDecorator(t));
        }
    }

    public IEnumerable<IView> Views => Enumerable.Empty<IView>();
    public IEnumerable<ISqlQuery> Queries => Enumerable.Empty<ISqlQuery>();
    public IEnumerable<IStoreFunction> Functions => Enumerable.Empty<IStoreFunction>();
    public IEnumerable<IStoreStoredProcedure> StoredProcedures => Enumerable.Empty<IStoreStoredProcedure>();
}