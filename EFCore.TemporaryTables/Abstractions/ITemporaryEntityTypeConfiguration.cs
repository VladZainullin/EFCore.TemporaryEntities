using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Abstractions;

public interface ITemporaryEntityTypeConfiguration<TEntity> :
    IEntityTypeConfiguration<TEntity> where TEntity : class
{
}