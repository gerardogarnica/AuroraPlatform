using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Aurora.Framework.Logging
{
    public static class LoggerHandlerConfiguration
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
            => builder.UseSerilog((context, loggerConfiguration)
                => ConfigureLogger(loggerConfiguration, context.Configuration, context.HostingEnvironment));

        private static LoggerConfiguration ConfigureLogger(
            LoggerConfiguration logger, IConfiguration configuration, IHostEnvironment environment)
        {
            var elasticsearchUri = new Uri(configuration.GetValue<string>("ElasticsearchConfiguration:Uri"));
            var elasticsearchIndexName = $"{configuration.GetValue<string>("ElasticsearchConfiguration:IndexName")}-{DateTime.UtcNow.ToString(DateFormat.YearMonth, "-")}";

            logger
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUri)
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = elasticsearchIndexName,
                    NumberOfShards = 2,
                    NumberOfReplicas = 1
                })
                .Enrich.WithProperty("Environment", environment.EnvironmentName)
                .Enrich.WithProperty("Application", environment.ApplicationName)
                .ReadFrom.Configuration(configuration);

            return logger;
        }
    }
}