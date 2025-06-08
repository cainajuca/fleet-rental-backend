using Fleet.Domain.Entities;

namespace Fleet.Api._3_Domain.Entities;
public class NotificationMessage : BaseEntity
{
    public required string Message { get; set; }
    public DateTime ReceivedAt { get; set; }
}
