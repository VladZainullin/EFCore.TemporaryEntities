using System.Reflection;
using EFCore.TemporaryTables.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Sample;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=123456",
                o => { o.UseTemporaryTables(); })
            .LogTo(Console.WriteLine, LogLevel.Information);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("Sample"));
        base.OnModelCreating(modelBuilder);
    }
}