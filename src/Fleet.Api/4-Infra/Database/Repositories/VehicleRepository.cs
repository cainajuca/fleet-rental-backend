using Fleet.Api._3_Domain.Entities;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Api._4_Infra.Database.Repositories;

public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context) { }

    public async Task<Vehicle?> GetByIdentifierAsync(string identifier) =>
        await _dbSet
            .SingleOrDefaultAsync(x => x.Identifier == identifier);
}
