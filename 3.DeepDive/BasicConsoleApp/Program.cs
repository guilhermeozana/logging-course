using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Console;

/*
using IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureLogging(x =>
        {
            x.AddJsonConsole();
        })
        .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Hello World!");

host.Run();
*/

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(options =>
    {
        options.IncludeScopes = false;
        options.TimestampFormat = "HH:mm:ss";
        options.JsonWriterOptions = new JsonWriterOptions()
        {
            Indented = true
        };
    });
    
    builder.AddFilter((provider, category, logLevel) =>
    {
        return provider!.Contains("Console") &&
               category!.Contains("Microsoft.Extensions.Hosting.Internal.Host") &&
               logLevel >= LogLevel.Debug;
    });

    builder
        .AddFilter("System", LogLevel.Debug)
        .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Information);

    builder.ClearProviders();
    builder.AddSystemdConsole();
    
    builder.SetMinimumLevel(LogLevel.Debug);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var name = "Nick";
var age = 30;

try
{
    throw new Exception("Something went wrong");
}
catch (Exception ex)
{
    logger.LogError(ex, "Failure during birthday of {Name} who is {Age}", name, age);
    throw;
}



