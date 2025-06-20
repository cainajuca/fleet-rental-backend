using Fleet.Domain.Entities;
using System.Linq.Expressions;

namespace Fleet.Domain.Interfaces.Repositories;

/// <summary>
/// Interface for user repository operations.
/// </summary>
public interface IUserRepository : IRepository<AppUser>
{
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<TResult?> GetByUsernameAsync<TResult>(string username, Expression<Func<AppUser, TResult>> selector);
}
