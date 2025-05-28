using EtlProject.Business.Settings;

namespace EtlProject.ServiceCollection;

public static class SettingsConfiguration
{
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<XmlSenderSettings>(configuration.GetSection("XmlSenderSettings"));
    }
}