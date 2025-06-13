using Fleet.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        #region IIS
        //var rabbitMqUrl = "amqp://guest:guest@localhost:5672";
        //var apiBaseUrl = "https://localhost:44338/";
        #endregion

        #region Docker
        var rabbitMqUrl = "amqp://guest:guest@rabbitmq:5672";
        var apiBaseUrl = "http://api:8080";
        #endregion

        var queueName = "vehicles.2024.queue";
        var exchangeName = "vehicles.exchange";

        services.AddHostedService(sp =>
            new NotificationWorker(rabbitMqUrl, queueName, apiBaseUrl, exchangeName));
    })
    .Build()
    .Run();
