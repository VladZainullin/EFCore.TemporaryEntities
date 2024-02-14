using EFCore.TemporaryTables.PostgreSQL.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample;

public sealed class PeopleConfiguration : ITemporaryEntityTypeConfiguration<People>
{
    public void Configure(EntityTypeBuilder<People> entityTypeBuilder)
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
    }
}