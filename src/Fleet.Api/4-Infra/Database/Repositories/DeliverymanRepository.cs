using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Domain.Entities;
using Fleet.Infra.Database;

namespace Fleet.Api._4_Infra.Database.Repositories;

public class DeliverymanRepository : Repository<Deliveryman>, IDeliverymanRepository
{
    public DeliverymanRepository(AppDbContext context) : base(context) { }
}
