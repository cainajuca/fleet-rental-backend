using Fleet.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var rabbitMqHost = Env.Get(Env.Keys.RabbitMqHost, "localhost");
        var apiBaseUrl = Env.Get(Env.Keys.ApiBaseUrl, "http://localhost:5000");

        var rabbitMqUrl = $"amqp://guest:guest@{rabbitMqHost}:5672";
        var queueName = "vehicles.2024.queue";
        var exchangeName = "vehicles.exchange";

        services.AddHostedService(sp =>
            new NotificationWorker(rabbitMqUrl, queueName, apiBaseUrl, exchangeName));
    })
    .Build()
    .Run();
