namespace Fleet.Domain.Entities;
public class NotificationMessage(string message) : BaseEntity
{
    public string Message { get; set; } = message;
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
}
