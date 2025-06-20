using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Infra.Database.Configurations;
public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Deliveryman)
            .WithMany(d => d.Rentals)
            .HasForeignKey(x => x.DeliverymanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Vehicle)
            .WithMany(v => v.Rentals)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
