using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore.TemporaryTables;

internal sealed class IncludeInMigrationTableDecorator : ITable
{
    private readonly ITable _table;

    public IncludeInMigrationTableDecorator(ITable table)
    {
        _table = table;
    }

    public IAnnotation? FindAnnotation(string name)
    {
        return _table.FindAnnotation(name);
    }

    public IEnumerable<IAnnotation> GetAnnotations()
    {
        return _table.GetAnnotations();
    }

    public object? this[string name] => throw new NotImplementedException();

    public IAnnotation? FindRuntimeAnnotation(string name)
    {
        return _table.FindRuntimeAnnotation(name);
    }

    public IEnumerable<IAnnotation> GetRuntimeAnnotations()
    {
        return _table.GetRuntimeAnnotations();
    }

    public IAnnotation AddRuntimeAnnotation(string name, object? value)
    {
        return _table.AddRuntimeAnnotation(name, value);
    }

    public IAnnotation SetRuntimeAnnotation(string name, object? value)
    {
        return _table.SetRuntimeAnnotation(name, value);
    }

    public IAnnotation? RemoveRuntimeAnnotation(string name)
    {
        return _table.RemoveRuntimeAnnotation(name);
    }

    public TValue GetOrAddRuntimeAnnotationValue<TValue, TArg>(string name, Func<TArg?, TValue> valueFactory,
        TArg? factoryArgument)
    {
        return _table.GetOrAddRuntimeAnnotationValue(name, valueFactory, factoryArgument);
    }

    IColumnBase? ITableBase.FindColumn(string name)
    {
        return FindColumn(name);
    }

    public IColumn? FindColumn(IProperty property)
    {
        return _table.FindColumn(property);
    }

    public IEnumerable<ITableMapping> EntityTypeMappings => _table.EntityTypeMappings;
    public IEnumerable<IColumn> Columns => _table.Columns;
    public bool IsExcludedFromMigrations => false; // Decorate
    public IEnumerable<IForeignKeyConstraint> ForeignKeyConstraints => _table.ForeignKeyConstraints;

    public IEnumerable<IForeignKeyConstraint> ReferencingForeignKeyConstraints =>
        _table.ReferencingForeignKeyConstraints;

    public IEnumerable<IUniqueConstraint> UniqueConstraints => _table.UniqueConstraints;
    public IPrimaryKeyConstraint? PrimaryKey => _table.PrimaryKey;
    public IEnumerable<ITableIndex> Indexes => _table.Indexes;
    public IEnumerable<ICheckConstraint> CheckConstraints => _table.CheckConstraints;
    public IEnumerable<ITrigger> Triggers => _table.Triggers;

    public IColumn? FindColumn(string name)
    {
        return _table.FindColumn(name);
    }

    IColumnBase? ITableBase.FindColumn(IProperty property)
    {
        return FindColumn(property);
    }

    public IEnumerable<IForeignKey> GetRowInternalForeignKeys(IEntityType entityType)
    {
        return _table.GetRowInternalForeignKeys(entityType);
    }

    public IEnumerable<IForeignKey> GetReferencingRowInternalForeignKeys(IEntityType entityType)
    {
        return _table.GetReferencingRowInternalForeignKeys(entityType);
    }

    public bool IsOptional(IEntityType entityType)
    {
        return _table.IsOptional(entityType);
    }

    public string Name => _table.Name;
    public string? Schema => _table.Schema;
    public IRelationalModel Model => _table.Model;
    public bool IsShared => _table.IsShared;
    IEnumerable<ITableMappingBase> ITableBase.EntityTypeMappings => EntityTypeMappings;

    IEnumerable<IColumnBase> ITableBase.Columns => Columns;
}