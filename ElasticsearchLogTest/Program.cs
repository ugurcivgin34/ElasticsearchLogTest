using Serilog.Sinks.Elasticsearch;
using Serilog;
using ElasticsearchLogTest.Services;
using ElasticsearchLogTest.Core.Logger;
using ElasticsearchLogTest.Exceptions;
using Serilog.Formatting.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var elasticsearchUrl = new Uri("http://localhost:9200"); // Ger�ek Elasticsearch URL'nizi kullan�n
builder.Services.AddSingleton(new ElasticsearchService(elasticsearchUrl));
builder.Services.AddSingleton<ILoggerService, SerilogLoggerService>(); // GeneralLoggerService kullan�m�

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUrl)
    {
        AutoRegisterTemplate = true,
        IndexDecider = (logEvent, offset) =>
        {
            // E�er IndexName �zelli�i varsa kullan, yoksa varsay�lan index format�n� kullan
            return logEvent.Properties.ContainsKey("IndexName") 
                ? logEvent.Properties["IndexName"].ToString().Replace("\"", "") 
                : $"logstash-{offset:yyyy.MM.dd}";
        },
        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
    })
    .CreateLogger();

builder.Logging.ClearProviders();



var app = builder.Build();

var loggerService = app.Services.GetRequiredService<ILoggerService>();
app.UseMiddleware<AuditLogMiddleware>();
app.UseMiddleware<ExceptionMiddleware>(loggerService);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
