using EtlProject.Business.Builders;
using EtlProject.Business.Interfaces;
using EtlProject.Business.Messaging;
using EtlProject.Business.Services;
using RabbitMQ.Client;

namespace EtlProject.ServiceCollection;

public static class ServiceConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceProcessingService, InvoiceProcessingService>();
        services.AddScoped<XmlBuilder>();
        services.AddScoped<RabbitMqListener>();
        services.AddSingleton(new ConnectionFactory { HostName = "localhost" });

    }
}