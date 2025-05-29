using BasicConsoleApp;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .SetMinimumLevel(LogLevel.Information)
        .AddConsole();
    //.AddJsonConsole();
});

ILogger logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Hello world!");

var age = 26;

//logger.LogInformation("Age: " + age);
//logger.LogInformation(string.Format("Age: {0}", age));
//logger.Log(LogLevel.Information, 0, "Hello world!");

logger.LogInformation("Age: {Age}", age.ToString());
logger.LogInformation(LogEvents.UserBirthday,"Age: {Age}", age.ToString());

