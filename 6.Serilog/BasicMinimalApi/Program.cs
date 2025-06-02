using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

ILogger logger = new LoggerConfiguration()
    .WriteTo.Console(theme:AnsiConsoleTheme.Code)
    .CreateLogger();

Log.Logger = logger;

builder.Services.AddSingleton(logger);

var app = builder.Build();

app.MapGet("/", (ILogger log) =>
{
    log.Information("Hello from the endpoint");
    return "Hello World!";
});

app.Run();
 