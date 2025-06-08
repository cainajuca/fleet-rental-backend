using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Infra.Database.Configurations;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Email)
            .IsRequired();

        builder
            .Property(x => x.PasswordHash)
            .IsRequired();

        builder
            .Property(x => x.Role)
            .IsRequired()
            .HasDefaultValue(UserRole.Deliveryman);
    }
}