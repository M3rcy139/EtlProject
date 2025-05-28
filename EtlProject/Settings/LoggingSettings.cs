using Serilog.Events;

namespace EtlProject.Settings;

public class LoggingSettings
{
    public string ApplicationName { get; set; }

    public LogEventLevel DefaultLogLevel { get; set; }
    public LogEventLevel AspNetCoreLogLevel { get; set; }
    public LogEventLevel ConsoleLogLevel { get; set; }
    public LogEventLevel FileLogLevel { get; set; }

    public string FilePath { get; set; } 
    public int RetainedFileCountLimit { get; set; }
}