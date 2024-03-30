using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryTables.Abstractions;

public interface ITemporaryEntityConfigurator
{
    public void Add<TEntity>(MulticastDelegate configure) where TEntity : class;
    
    public void Configure<TEntity>(ModelBuilder modelBuilder) where TEntity : class;
}