using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.PostgreSQL.Abstractions;

public interface ITemporaryEntityTypeConfiguration<TEntity> :
    IEntityTypeConfiguration<TEntity> where TEntity : class
{
}