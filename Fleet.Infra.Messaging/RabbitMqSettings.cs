namespace Fleet.Infra.Messaging;

public class RabbitMqSettings
{
    public required string Uri { get; set; }
    public required string ExchangeName { get; set; }
}
