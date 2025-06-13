using Fleet.Api._3_Domain.Interfaces.Services;
using RabbitMQ.Client;

namespace Fleet.Api._4_Infra.Messaging;
public class RabbitMqPublisher : IMessagePublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly string _exchangeName;

    public RabbitMqPublisher(
        ILogger<RabbitMqPublisher> logger,
        string rabbitMqUri,
        string exchangeName)
    {
        _logger = logger;
        _exchangeName = exchangeName;

        var factory = new ConnectionFactory { Uri = new Uri(rabbitMqUri) };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(
            exchange: _exchangeName,
            type: ExchangeType.Topic,
            durable: true);
    }

    public Task PublishAsync(string routingKey, ReadOnlyMemory<byte> body)
    {
        _logger.LogDebug("Publishing message to exchange {Exchange} with routingKey {RoutingKey} (size = {Size} bytes)",
            _exchangeName, routingKey, body.Length);

        var props = _channel.CreateBasicProperties();
        props.Persistent = true;

        _channel.BasicPublish(
            exchange: _exchangeName,
            routingKey: routingKey,
            basicProperties: props,
            body: body);

        _logger.LogInformation("Message published. Exchange = {Exchange}, RoutingKey = {RoutingKey}",
            _exchangeName, routingKey);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposing RabbitMqPublisher (closing channel and connection)");

        _channel?.Close();
        _connection?.Close();
    }
}