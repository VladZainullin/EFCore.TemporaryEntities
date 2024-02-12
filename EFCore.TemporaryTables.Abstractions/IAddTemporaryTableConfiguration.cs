namespace EFCore.TemporaryTables.Abstractions;

public interface IAddTemporaryTableConfiguration
{
    public void Add<TEntity>(MulticastDelegate configure) where TEntity : class;
}