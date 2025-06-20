using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;

namespace Fleet.Infra.Database.Repositories;

public class RentalRepository : Repository<Rental>, IRentalRepository
{
    public RentalRepository(AppDbContext context) : base(context) { }
}
