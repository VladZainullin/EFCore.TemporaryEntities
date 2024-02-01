using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore.TemporaryTables.Adapters;

public abstract class TemporaryTable<TEntity> :
    DbSet<TEntity>,
    IEnumerable<TEntity>,
    IQueryable,
    IInfrastructure<IServiceProvider>,
    IListSource where TEntity : class
{
    protected readonly DbContext Context;

    internal TemporaryTable(DbContext context)
    {
        Context = context;
    }
    
    protected IDesignTimeModel DesignTimeModel => Context.GetService<IDesignTimeModel>();

    private DbSet<TEntity> Set => Context.Set<TEntity>();

    public abstract Task CreateAsync(CancellationToken cancellationToken = default);

    public abstract Task DropAsync(CancellationToken cancellationToken = default);

    public abstract Task<bool> ExistsAsync(CancellationToken cancellationToken = default);

    #region Properies from DbSet

    Type IQueryable.ElementType => ((IQueryable)Set).ElementType;

    Expression IQueryable.Expression => ((IQueryable)Set).Expression;

    IQueryProvider IQueryable.Provider => ((IQueryable)Set).Provider;

    IServiceProvider IInfrastructure<IServiceProvider>.Instance =>
        ((IInfrastructure<IServiceProvider>)Set).Instance;

    bool IListSource.ContainsListCollection => ((IListSource)Set).ContainsListCollection;

    public override IEntityType EntityType => Set.EntityType;

    #endregion

    #region Methods from DbSet

    #region Find methods

    public override TEntity? Find(params object?[]? keyValues)
    {
        return Context.Set<TEntity>().Find(keyValues);
    }

    public override LocalView<TEntity> Local => Set.Local;

    public override ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
    {
        return Set.FindAsync(keyValues);
    }

    public override ValueTask<TEntity?> FindAsync(
        object?[]? keyValues,
        CancellationToken cancellationToken)
    {
        return Set.FindAsync(keyValues, cancellationToken);
    }

    #endregion

    #region Add methods

    public override EntityEntry<TEntity> Add(TEntity entity)
    {
        return Set.Add(entity);
    }

    public override ValueTask<EntityEntry<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        return Set.AddAsync(entity, cancellationToken);
    }

    #endregion

    #region Attach methods

    public override EntityEntry<TEntity> Attach(TEntity entity)
    {
        return Set.Attach(entity);
    }

    #endregion

    #region Remove methods

    public override EntityEntry<TEntity> Remove(TEntity entity)
    {
        return Set.Remove(entity);
    }

    #endregion

    #region Update methods

    public override EntityEntry<TEntity> Update(TEntity entity)
    {
        return Set.Update(entity);
    }

    #endregion

    #region Add range methods

    public override void AddRange(params TEntity[] entities)
    {
        Set.AddRange(entities);
    }

    public override Task AddRangeAsync(params TEntity[] entities)
    {
        return Set.AddRangeAsync(entities);
    }

    public override Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        return Set.AddRangeAsync(entities, cancellationToken);
    }

    #endregion

    #region Attach range methods

    public override void AttachRange(params TEntity[] entities)
    {
        Set.AttachRange(entities);
    }

    public override void AttachRange(IEnumerable<TEntity> entities)
    {
        Set.AttachRange(entities);
    }

    #endregion

    #region Remove range methods

    public override void RemoveRange(params TEntity[] entities)
    {
        Set.RemoveRange(entities);
    }

    public override void RemoveRange(IEnumerable<TEntity> entities)
    {
        Set.RemoveRange(entities);
    }

    #endregion

    #region Update range methods

    public override void UpdateRange(params TEntity[] entities)
    {
        Set.UpdateRange(entities);
    }

    public override void UpdateRange(IEnumerable<TEntity> entities)
    {
        Set.UpdateRange(entities);
    }

    #endregion

    #region Entry methods

    public override EntityEntry<TEntity> Entry(TEntity entity)
    {
        return Set.Entry(entity);
    }

    #endregion

    #region Get enumerator methods

    IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
    {
        return ((IEnumerable<TEntity>)Set).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<TEntity>)Set).GetEnumerator();
    }

    #endregion

    #region Get async enumerator methods

    public override IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return ((IAsyncEnumerable<TEntity>)Set).GetAsyncEnumerator(cancellationToken);
    }

    #endregion

    #region Get list methods

    IList IListSource.GetList()
    {
        return ((IListSource)Set).GetList();
    }

    #endregion

    #region Object methods

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string? ToString()
    {
        return Set.ToString();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool Equals(object? obj)
    {
        return Set.Equals(obj);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    #endregion

    #endregion
}