namespace Fleet.Api._3_Domain.Interfaces.Services;
public interface IMessagePublisher
{
    Task PublishAsync(string routingKey, ReadOnlyMemory<byte> body);
}
