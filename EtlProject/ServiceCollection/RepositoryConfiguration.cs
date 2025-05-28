using EtlProject.DataAccess.Interfaces;
using EtlProject.DataAccess.Repositories;

namespace EtlProject.ServiceCollection;

public static class RepositoryConfiguration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProcessingLogRepository, ProcessingLogRepository>();
        services.AddScoped<IJsonMessageRepository, JsonMessageRepository>();
    }
}