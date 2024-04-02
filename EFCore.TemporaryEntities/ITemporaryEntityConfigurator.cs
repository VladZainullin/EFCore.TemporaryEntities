using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.TemporaryEntities;

public interface ITemporaryEntityConfigurator
{
    void Add<TEntity>(Action<EntityTypeBuilder<TEntity>> configure) where TEntity : class;

    void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class;
}