using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;

namespace Fleet.Infra.Database;
public static class SeedData
{
    public static void SeedAdmin(AppDbContext ctx)
    {
        var adminId = new Guid("0025B6CF-22A6-4C86-9CAE-9CA6ED6371CD");
        const string user = "admin";
        const string passwordHash = "AQAAAAIAAYagAAAAEG9pSGbv3U78phcTrZb8z2CE7dY5vGCd9Gl+H2RmZIHodObCMVGTgkoikb6K2RZjrw=="; // P4ssword!@

        if (!ctx.AppUser.Any(u => u.Username == user))
        {
            var now = DateTime.UtcNow;

            var admin = new AppUser
            {
                Id = adminId,
                Username = user,
                PasswordHash = passwordHash,
                
                Name = "Admin User",
                BirthDate = now,
                CreatedAt = now,

                Role = UserRole.Admin,
            };

            ctx.AppUser.Add(admin);
            ctx.SaveChanges();
        }
    }
}
