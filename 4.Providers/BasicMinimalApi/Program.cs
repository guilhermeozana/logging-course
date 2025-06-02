using BasicMinimalApi;
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Logging.ClearProviders();
    //builder.Logging.AddConsole();
    builder.Logging.AddProvider(new MyLoggerProvider());
}
else
{
    builder.Logging.ClearProviders();
    builder.Logging.AddApplicationInsights(
        configureTelemetryConfiguration: teleConfig =>
            teleConfig.ConnectionString =
                "InstrumentationKey=<INSTRUMENTATION_KEY>;IngestionEndpoint=<INGESTION_ENDPOINT>;LiveEndpoint=<LIVE_ENDPOINT>",
    
        configureApplicationInsightsLoggerOptions: _ => { }
    );
}



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
