using EtlProject.Settings;
using Serilog;
using Serilog.Formatting.Json;

namespace EtlProject.ServiceCollection;

public static class LoggingConfiguration
{
    public static void ConfigureLogging(this IHostBuilder hostBuilder, IConfiguration configuration)
    {
        var logSettings = configuration.GetSection("Logging").Get<LoggingSettings>();
            
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(logSettings!.DefaultLogLevel)
            .MinimumLevel.Override("Microsoft.AspNetCore", logSettings.AspNetCoreLogLevel)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", logSettings.ApplicationName)
            .WriteTo.Console(restrictedToMinimumLevel: logSettings.ConsoleLogLevel)
            .WriteTo.File(
                new JsonFormatter(),
                path: logSettings.FilePath,
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: logSettings.FileLogLevel,
                retainedFileCountLimit: logSettings.RetainedFileCountLimit)
            .CreateLogger();

        hostBuilder.UseSerilog();
    }
}