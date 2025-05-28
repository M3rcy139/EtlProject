using EtlProject.Business.Mappers;

namespace EtlProject.ServiceCollection;

public static class MapperConfiguration
{
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddScoped<InvoiceJsonMapper>();
    }
}