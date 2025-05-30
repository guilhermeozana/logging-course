using System.Text.Json;
using BasicConsoleApp;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddJsonConsole(x =>
    {
       x.IncludeScopes = true;
    });
    
    builder.SetMinimumLevel(LogLevel.Warning);
});

ILogger logger = loggerFactory.CreateLogger<Program>();

var paymentData = new PaymentData
{
    PaymentId = 1,
    Amount = 15.99m
};

var paymentId = 1;
var amount = 15.99;
var date = DateTime.Now;

while (true)
{
    logger.LogInformation(
        "New Payment with data {PaymentData}", JsonSerializer.Serialize(paymentData));
    await Task.Delay(1000);
}

class PaymentData
{
    public int PaymentId { get; set; }
    
    public decimal Amount { get; set; }
    
    
}
