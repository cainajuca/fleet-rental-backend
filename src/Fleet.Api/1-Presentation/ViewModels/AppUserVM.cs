using Fleet.Domain.Constants.Enums;

namespace Fleet.Api._1_Presentation.ViewModels;

public class AppUserVM
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
