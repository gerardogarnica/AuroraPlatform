using Serilog.Events;

namespace Aurora.Framework.Logging.Loggers
{
    internal class ElasticsearchLoggerConfiguration
    {
        internal bool Enabled { get; set; } = false;
        internal LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;
    }
}