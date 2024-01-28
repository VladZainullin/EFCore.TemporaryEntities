using EFCore.TemporaryTables.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Sample;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(Console.WriteLine, LogLevel.Information)
            .UseSqlite("DataSource=/Users/vadislavzainullin/RiderProjects/EFCore.TemporaryTables/Sample/app.db")
            .UseTemporaryTables();
            //.UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=123456;");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<People>();

        modelBuilder.ToTemporaryTable<Projection>();
        
        base.OnModelCreating(modelBuilder);
    }
}