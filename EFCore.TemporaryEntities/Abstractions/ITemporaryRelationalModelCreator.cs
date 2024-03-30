using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCore.TemporaryTables.Abstractions;

public interface ITemporaryRelationalModelCreator
{
    IRelationalModel Create<TEntity>() where TEntity : class;
}