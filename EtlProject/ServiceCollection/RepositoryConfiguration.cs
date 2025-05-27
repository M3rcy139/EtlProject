using EtlProject.DataAccess.Interfaces;
using EtlProject.DataAccess.Repositories;

namespace EtlProject.ServiceCollection;

public static class RepositoryConfiguration
{
    public static void AddRepositoryConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<IProcessingLogRepository, ProcessingLogRepository>();
    }
}