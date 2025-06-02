using System.Text.Json;
using BasicConsoleApp;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
       x.IncludeScopes = true;
       x.JsonWriterOptions = new JsonWriterOptions { Indented = true };
    });
    
    builder.SetMinimumLevel(LogLevel.Warning);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentId = 1;
var amount = 15.99;

if (logger.IsEnabled(LogLevel.Information))
{
    logger.LogInformation("Payment Id: {PaymentId}", paymentId);
}

using (logger.BeginScope("{PaymentId}", paymentId))
{
    try
    {
        logger.LogInformation("New payment for ${Total}", amount);
        //processing
    }
    finally
    {
        logger.LogInformation("Payment processing completed");
    }
}
