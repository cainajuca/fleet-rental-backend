using Fleet.Api._3_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Infra.Database.Configurations;
public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(v => v.Identifier)
           .IsUnique();

        builder.Property(v => v.Identifier)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.LicensePlate)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .HasMany(v => v.Rentals)
            .WithOne(r => r.Vehicle)
            .HasForeignKey(r => r.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}