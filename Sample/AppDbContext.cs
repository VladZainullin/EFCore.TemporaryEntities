using EFCore.TemporaryTables.Extensions;
using EFCore.TemporaryTables.PostgreSQL;
using EFCore.TemporaryTables.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Sample;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            // .UseInMemoryDatabase("app", o =>
            // {
            // })
            .UseNpgsql("Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=123456", o =>
            {
                o.UseTemporaryTables();
            })
            // .UseSqlite("DataSource=/Users/vadislavzainullin/RiderProjects/EFCore.TemporaryTables/Sample/app.db", o =>
            // {
            //     o.UseTemporaryTables();
            // })
            .LogTo(Console.WriteLine, LogLevel.Information);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.TemporaryEntity<People>(this, entityTypeBuilder =>
        {
            entityTypeBuilder.HasKey(p => p.Id);

            entityTypeBuilder
                .OwnsOne(
                    p => p.Identification,
                    ownedNavigationBuilder =>
                    {
                        ownedNavigationBuilder.ToTable("temp_identification");

                        ownedNavigationBuilder
                            .HasIndex(i => i.Gender)
                            .IsDescending();
                    })
                .OwnsOne(
                    p => p.Family,
                    ownedNavigationBuilder => { ownedNavigationBuilder.HasIndex(f => f.HasPartner); })
                .OwnsOne(p => p.Work, ownedNavigationBuilder => { ownedNavigationBuilder.OwnsOne(w => w.Address); });

            entityTypeBuilder.OwnsOne(p => p.Address);
            entityTypeBuilder.ToTable("temp_peoples");
        });

        base.OnModelCreating(modelBuilder);
    }
}