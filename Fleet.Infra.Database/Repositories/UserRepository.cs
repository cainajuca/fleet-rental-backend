using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fleet.Infra.Database.Repositories;
public class UserRepository : Repository<AppUser>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<AppUser?> GetUserByUsernameAsync(string username) =>
        await _dbSet
            .Include(u => u.Deliveryman)
            .SingleOrDefaultAsync(x => x.Username == username);

    public async Task<AppUser?> GetByUsernameAsync(string username) =>
        await _dbSet.SingleOrDefaultAsync(u => u.Username == username);

    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _dbSet.AnyAsync(u => u.Username == username);

    public async Task<TResult?> GetByUsernameAsync<TResult>(string username, Expression<Func<AppUser, TResult>> selector) =>
        await _dbSet
            .Where(x => x.Username == username)
            .Select(selector)
            .FirstOrDefaultAsync();
}
