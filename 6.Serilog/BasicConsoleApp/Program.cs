using Serilog;
using Serilog.Context;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogTimings.Extensions;

ILogger logger = new LoggerConfiguration()
    //.WriteTo.Console(theme:AnsiConsoleTheme.Code)
    .WriteTo.Console(new JsonFormatter())
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
    .Enrich.FromLogContext()
    .Destructure.ByTransforming<Payment>(x => new
    {
        x.PaymentId, x.UserId 
    })
    .CreateLogger();

Log.Logger = logger;

var payment = new Payment
{
    PaymentId = 1,
    UserId = Guid.NewGuid(),
    OccuredAt = DateTime.UtcNow
};

using (LogContext.PushProperty("PaymentId", payment.PaymentId))
{
    logger.Information("New payment by user with id {UserId}", payment.UserId );
}

var paymentData = new Dictionary<string, object>
{
    { "PaymentId", payment.PaymentId },
    { "UserId", payment.UserId },
    { "OccuredAt", payment.OccuredAt }
};

/*
using (logger.TimeOperation("Processing payment with id: {PaymentId}", payment.PaymentId))
{
    await Task.Delay(50);
    logger.Information("New payment with data: {PaymentData}", paymentData);
}
*/

var op = logger.BeginOperation("Processing payment with id: {PaymentId}", payment.PaymentId);
    
await Task.Delay(50);
logger.Information("New payment with data: {PaymentData}", paymentData); 

op.Complete();
//op.Abandon(); //it returns a warning log

logger.Information("New payment with data: {PaymentData}", paymentData);
logger.Information("New payment with data: {$PaymentData}", paymentData);
logger.Information("New payment with data: {@PaymentData}", payment);
logger.Information("New payment with data: {$PaymentData}", payment);


Log.CloseAndFlush();
