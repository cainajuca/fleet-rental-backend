using Fleet.Domain.Constants.Enums;

namespace Fleet.Domain.Entities;

public class AppUser : BaseEntity
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public UserRole? Role { get; set; }
}
