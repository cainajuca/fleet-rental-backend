using Fleet.Api._3_Domain.Entities;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Api._4_Infra.Database.Repositories;

public class RentalRepository : Repository<Rental>, IRentalRepository
{
    public RentalRepository(AppDbContext context) : base(context) { }
}
