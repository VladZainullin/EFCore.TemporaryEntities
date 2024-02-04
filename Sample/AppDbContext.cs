using EFCore.TemporaryTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Sample;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("DataSource=/Users/vadislavzainullin/RiderProjects/EFCore.TemporaryTables/Sample/app.db")
            .LogTo(Console.WriteLine, LogLevel.Information);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<People>(entityTypeBuilder =>
        {
            entityTypeBuilder.HasKey(p => p.Id);

            entityTypeBuilder
                .OwnsOne(
                    p => p.Identification,
                    ownedNavigationBuilder =>
                    {
                        ownedNavigationBuilder
                            .HasIndex(i => i.Gender)
                            .IsDescending();

                        ownedNavigationBuilder
                            .HasIndex(i => i.DateOfBirth)
                            .IsUnique();
                    })
                .OwnsOne(
                    p => p.Family,
                    ownedNavigationBuilder => { ownedNavigationBuilder.HasIndex(f => f.HasPartner); })
                .OwnsOne(p => p.Work, ownedNavigationBuilder => { ownedNavigationBuilder.OwnsOne(w => w.Address); });

            entityTypeBuilder.OwnsOne(p => p.Address);
            entityTypeBuilder.ToTemporaryTable("temp_peoples");
        });

        base.OnModelCreating(modelBuilder);
    }
}