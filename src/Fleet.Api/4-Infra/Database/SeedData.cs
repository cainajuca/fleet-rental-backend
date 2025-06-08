using Fleet.Domain.Constants.Enums;
using Fleet.Domain.Entities;
using Fleet.Infra.Database;

namespace Fleet.Api._4_Infra.Database;
public static class SeedData
{
    public static void SeedAdmin(AppDbContext ctx)
    {
        var adminId = new Guid("0025B6CF-22A6-4C86-9CAE-9CA6ED6371CD");
        const string user = "admin";
        const string email = "admin@admin.com";
        const string passwordHash = "AQAAAAIAAYagAAAAEG9pSGbv3U78phcTrZb8z2CE7dY5vGCd9Gl+H2RmZIHodObCMVGTgkoikb6K2RZjrw=="; // P4ssword!@

        if (!ctx.AppUser.Any(u => u.Username == user))
        {
            var admin = new AppUser
            {
                Id = adminId,
                Username = user,
                Email = email,
                PasswordHash = passwordHash,
                Role = UserRole.Admin,
            };
            ctx.AppUser.Add(admin);
            ctx.SaveChanges();
        }
    }
}
