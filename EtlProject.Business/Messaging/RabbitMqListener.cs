using System.Text;
using EtlProject.Business.Interfaces;
using EtlProject.Core.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EtlProject.Business.Messaging;

public class RabbitMqListener : IDisposable
{
    private readonly ILogger<RabbitMqListener> _logger;
    private readonly IServiceProvider _services;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitMqListener(ILogger<RabbitMqListener> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    public void StartListening(string queueName)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            _logger.LogInformation(InfoMessages.ReceivedMessage, json);

            using var scope = _services.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IInvoiceProcessingService>();
            await processor.ProcessMessageAsync(json);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        _logger.LogInformation(InfoMessages.StartedListeningToQueue, queueName);
    }
    
    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}