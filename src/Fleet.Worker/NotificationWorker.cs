using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Fleet.Worker;
public class NotificationWorker : BackgroundService
{
    private readonly string _rabbitMqUrl;
    private readonly string _queueName;
    private readonly string _exchangeName;

    private IConnection? _connection;
    private IModel? _channel;
    private readonly NotificationService.NotificationServiceClient _grpcClient;


    public NotificationWorker(string rabbitMqUrl, string queueName, string apiBaseUrl, string exchangeName)
    {
        _rabbitMqUrl = rabbitMqUrl;
        _queueName = queueName;
        _exchangeName = exchangeName;

        _httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };

        Console.WriteLine($"[Worker] Service started successfully");
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory { Uri = new Uri(_rabbitMqUrl) };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        // Assures that the exchange exists before consuming messages
        _channel.ExchangeDeclare(
            exchange: _exchangeName, // vehicles.exchange
            type: ExchangeType.Topic,
            durable: true,
            autoDelete: false,
            arguments: null
        );

        // Assures that the queue exists before consuming messages
        _channel.QueueDeclare(
            queue: _queueName, // vehicles.2024.queue
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _channel.QueueBind(
            queue: _queueName,
            exchange: _exchangeName,
            routingKey: "vehicle.created.2024");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel!);

        consumer.Received += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var payload = JsonSerializer.Serialize(new { Message = message });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            try
            {
                var resp = await _httpClient.PostAsync("NotificationMessage", content, stoppingToken);
                if (resp.IsSuccessStatusCode)
                    // only acknowledge the message if the API call was successful
                    _channel!.BasicAck(ea.DeliveryTag, multiple: false);
                else
                    Console.WriteLine($"[Worker] API retornou {(int)resp.StatusCode}");
                // optional: let the message remain unacknowledged for retry
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Worker] Erro ao chamar API: {ex.Message}");
            }
        };

        _channel!.BasicConsume(
            queue: _queueName,
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        _httpClient.Dispose();
        base.Dispose();
    }
}
