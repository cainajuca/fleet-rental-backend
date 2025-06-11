using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Domain.Entities;
using Fleet.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Api._4_Infra.Database.Repositories;
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
}
