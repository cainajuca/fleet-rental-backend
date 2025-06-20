using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Infra.Database.Repositories;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context) { }

    public async Task<Vehicle?> GetByIdentifierAsync(string identifier) =>
        await _dbSet
            .Include(x => x.Rentals)
            .SingleOrDefaultAsync(x => x.Identifier == identifier);
}
