using Fleet.Domain.Entities;

namespace Fleet.Api._3_Domain.Repositories;

/// <summary>
/// Interface for user repository operations.
/// </summary>
public interface IUserRepository : IRepository<AppUser>
{
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<bool> ExistsByUsernameAsync(string username);
}
