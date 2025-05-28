using EtlProject.BackgroundServices;
using EtlProject.ServiceCollection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Host.ConfigureLogging(configuration);

try
{
    Log.Information("Initializing the application.");


    services.AddDbServices(configuration);
    
    services.AddMappers();
    services.AddSenders();
    services.AddHttpClient();
    
    services.AddServices();
    services.AddRepositories();

    services.AddHostedService<RabbitMqWorker>();

    var app = builder.Build();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application is stopped due to an exception.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}