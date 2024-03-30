using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample;

public sealed class CustomerConfiguration : ITemporaryEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder
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

        builder.OwnsOne(p => p.Address);
        builder.ToTable("temp_customers");
    }
}