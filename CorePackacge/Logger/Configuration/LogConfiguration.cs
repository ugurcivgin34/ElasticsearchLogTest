using Microsoft.AspNetCore.Builder;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CorePackacge.Logger.Abstract;
using CorePackacge.Logger.SeriLogConcrete;
using Serilog.Core;
using Serilog.Events;

namespace CorePackacge.Logger.Configuration
{
    public class LogConfiguration
    {
        public static void ConfigureLogging(WebApplicationBuilder builder)
        {
            var elasticsearchUrl = new Uri("http://localhost:9200"); // Gerçek URL'nizi kullanın
            var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);
            Log.Logger = new LoggerConfiguration()
     .MinimumLevel.ControlledBy(levelSwitch) // Log seviyesini değiştirmek için levelSwitch.MinimumLevel = LogEventLevel.Debug; şeklinde kullanabilirsiniz.
     .Filter.ByIncludingOnly(logEvent =>
         logEvent.Level >= LogEventLevel.Warning ||
         (logEvent.Level == LogEventLevel.Information && logEvent.Properties.ContainsKey("Application")) // Application özelliği olan logları kaydet
     )
                 .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUrl)
                {
                    AutoRegisterTemplate = true,
                    IndexDecider = (logEvent, offset) =>
                    {
                        return logEvent.Properties.ContainsKey("IndexName")
                            ? logEvent.Properties["IndexName"].ToString().Replace("\"", "")
                            : $"logstash-{offset:yyyy.MM.dd}";
                    },
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
                })
                .CreateLogger();

            builder.Services.AddSingleton<ILoggerService, SerilogLoggerService>();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();
        }
    }
}
