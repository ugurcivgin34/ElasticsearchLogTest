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

var elasticsearchUrl = new Uri("http://localhost:9200"); // Burada ger�ek Elasticsearch URL'nizi kullan�n
builder.Services.AddSingleton(new ElasticsearchService(elasticsearchUrl));
builder.Services.AddSingleton<ILoggerService, SerilogLoggerService>();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUrl)
    {
        AutoRegisterTemplate = true, // Elasticsearch'te index olu�turulmas�n� sa�lar
        IndexFormat = "logstash-{0:yyyy.MM.dd}", // Index format�
        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true) // Exception'lar� JSON olarak loglar
    })
    .CreateLogger();

builder.Logging.ClearProviders();


var app = builder.Build();

var loggerService = app.Services.GetRequiredService<ILoggerService>();
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
