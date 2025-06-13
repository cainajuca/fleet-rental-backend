using Fleet.Domain.Entities;

namespace Fleet.Api._3_Domain.Entities;
public class NotificationMessage(string message) : BaseEntity
{
    public string Message { get; set; } = message;
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
}
