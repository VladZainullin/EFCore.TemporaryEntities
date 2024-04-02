using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryEntities;

public interface ITemporaryEntityConfigurator
{
    void Add<TEntity>(MulticastDelegate configure) where TEntity : class;
    
    void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class;
}