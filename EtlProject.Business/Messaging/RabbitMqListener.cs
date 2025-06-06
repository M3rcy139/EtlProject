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
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private IModel? _channel;

    public RabbitMqListener(ILogger<RabbitMqListener> logger, IServiceProvider services, ConnectionFactory factory)
    {
        _logger = logger;
        _services = services;
        _factory = factory;
    }

    public void StartListening(string queueName)
    {
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);

        StartConsumer(queueName);
        
        _logger.LogInformation(InfoMessages.StartedListeningToQueue, queueName);
    }
    
    private void StartConsumer(string queueName)
    {
        if (_channel == null) return;

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnMessageReceived;
        
        _channel.BasicConsume(
            queue: queueName,
            autoAck: true,
            consumer: consumer);
    }
    
    private async void OnMessageReceived(object? model, BasicDeliverEventArgs ea)
    {
        try
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            _logger.LogInformation(InfoMessages.ReceivedMessage, json);

            using var scope = _services.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IInvoiceProcessingService>();
            await processor.ProcessMessageAsync(json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorMessages.ErrorMessageFromRabbitMq);
        }
    }
    
    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}