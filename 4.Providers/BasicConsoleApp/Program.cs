using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using var channel = new InMemoryChannel();

try
{
    IServiceCollection services = new ServiceCollection();
    
    services.Configure<TelemetryConfiguration>(x => x.TelemetryChannel = channel);
    services.AddLogging(x => x.AddApplicationInsights(
        configureTelemetryConfiguration: teleConfig =>
            teleConfig.ConnectionString =
                "InstrumentationKey=<INSTRUMENTATION_KEY>;IngestionEndpoint=<INGESTION_ENDPOINT>;LiveEndpoint=<LIVE_ENDPOINT>\n",
        configureApplicationInsightsLoggerOptions: _ => { }
    ));

    var serviceProvider = services.BuildServiceProvider();
    
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Hello from console app!");
}
finally
{
    await channel.FlushAsync(default);
    await Task.Delay(1000);
}