using EtlProject.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace EtlProject.ServiceCollection;

public static class DbConfiguration
{
    public static void AddDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EtlDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString(nameof(EtlDbContext)),
                b => b.MigrationsAssembly("EtlProject.Migrations")));
    }
}