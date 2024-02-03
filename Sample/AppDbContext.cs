using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace Sample;

public sealed class AppDbContext : DbContext
{
    public Action<EntityTypeBuilder<People>> EntityTypeBuilder { get; private set; } = default!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            //.UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=123456;")
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
            entityTypeBuilder.Metadata.SetIsTableExcludedFromMigrations(true);
        });

        base.OnModelCreating(modelBuilder);
    }
}