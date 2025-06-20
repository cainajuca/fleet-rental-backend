using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces.Repositories;

namespace Fleet.Infra.Database.Repositories;

public class NotificationMessageRepository : Repository<NotificationMessage>, INotificationMessageRepository
{
    public NotificationMessageRepository(AppDbContext context) : base(context) { }
}
