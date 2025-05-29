using EtlProject.Business.External.Senders;

namespace EtlProject.ServiceCollection;

public static class SenderConfiguration
{
    public static void AddSenders(this IServiceCollection services)
    {
        services.AddScoped<XmlSender>();
    }
}