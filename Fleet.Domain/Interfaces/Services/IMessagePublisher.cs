namespace Fleet.Domain.Interfaces.Services;
public interface IMessagePublisher
{
    Task PublishAsync(string routingKey, ReadOnlyMemory<byte> body);
}
