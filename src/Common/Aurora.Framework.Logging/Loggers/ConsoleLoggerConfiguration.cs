using Serilog.Events;

namespace Aurora.Framework.Logging.Loggers
{
    internal class ConsoleLoggerConfiguration
    {
        internal bool Enabled { get; set; } = true;
        internal LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
    }
}