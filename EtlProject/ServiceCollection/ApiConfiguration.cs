using System.Text.Json.Serialization;

namespace EtlProject.ServiceCollection;

public static class ApiConfiguration
{
    public static void AddControllersAndSwagger(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

        services.AddRouting();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}