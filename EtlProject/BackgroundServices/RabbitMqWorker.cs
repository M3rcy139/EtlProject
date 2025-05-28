using EtlProject.Business.Messaging;
using EtlProject.Business.Services;

namespace EtlProject.BackgroundServices;

public class RabbitMqWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private IServiceScope? _scope;
    private RabbitMqListener? _listener;

    public RabbitMqWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _scope = _scopeFactory.CreateScope();
        _listener = _scope.ServiceProvider.GetRequiredService<RabbitMqListener>();
        _listener.StartListening("invoices");

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _listener?.Dispose();     
        _scope?.Dispose();        
        base.Dispose();
    }
}