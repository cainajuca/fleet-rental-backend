using Fleet.Api._3_Domain.Entities;
using Fleet.Api._3_Domain.Interfaces.Repositories;
using Fleet.Infra.Database;

namespace Fleet.Api._4_Infra.Database.Repositories;

public class NotificationMessageRepository : Repository<NotificationMessage>, INotificationMessageRepository
{
    public NotificationMessageRepository(AppDbContext context) : base(context) { }
}
