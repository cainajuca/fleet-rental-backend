using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;

namespace Fleet.Infra.Database.Repositories;

public class DeliverymanRepository : Repository<Deliveryman>, IDeliverymanRepository
{
    public DeliverymanRepository(AppDbContext context) : base(context) { }
}
