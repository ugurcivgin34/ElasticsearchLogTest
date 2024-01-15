using Serilog.Sinks.Elasticsearch;
using Serilog;
using ElasticsearchLogTest.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var elasticsearchUrl = new Uri("http://localhost:9200"); // Burada ger�ek Elasticsearch URL'nizi kullan�n
builder.Services.AddSingleton(new ElasticsearchService(elasticsearchUrl));

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
      .WriteTo.Seq("http://localhost:5341")
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUrl)
    {
        AutoRegisterTemplate = true, // Elasticsearch'te index olu�turulmas�n� sa�lar
        IndexFormat = "logstash-{0:yyyy.MM.dd}" // Index format�

    })
    .CreateLogger();

builder.Logging.ClearProviders();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
