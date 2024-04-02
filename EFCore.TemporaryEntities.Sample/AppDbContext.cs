using EFCore.TemporaryEntities.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore.TemporaryEntities.Sample;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("DataSource=/Users/vadislavzainullin/RiderProjects/Base/Sample/app.db", options =>
            {
                options.UseTemporaryEntities();
            })
            .LogTo(Console.WriteLine, LogLevel.Information);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.TemporaryEntity<People>(entityTypeBuilder =>
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