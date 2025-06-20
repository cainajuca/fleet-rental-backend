using Fleet.Domain.Constants.Enums;

namespace Fleet.Domain.Entities;

public class AppUser : BaseEntity
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public UserRole? Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Deliveryman? Deliveryman { get; set; }
}
