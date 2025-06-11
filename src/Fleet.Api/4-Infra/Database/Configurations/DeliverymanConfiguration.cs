using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Infra.Database.Configurations;
public class DeliverymanConfiguration : IEntityTypeConfiguration<Deliveryman>
{
    public void Configure(EntityTypeBuilder<Deliveryman> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasIndex(x => x.AppUserId)
            .IsUnique();

        builder
            .HasIndex(x => x.Cnpj)
            .IsUnique();

        builder
            .HasIndex(x => x.CnhNumber)
            .IsUnique();

        builder
            .Property(x => x.Cnpj)
            .IsRequired()
            .HasMaxLength(14);

        builder
            .Property(x => x.CnhNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder
            .HasOne(x => x.AppUser)
            .WithOne(x => x.Deliveryman)
            .HasForeignKey<Deliveryman>(x => x.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(d => d.Rentals)
            .WithOne(x => x.Deliveryman)
            .HasForeignKey(x => x.DeliverymanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
