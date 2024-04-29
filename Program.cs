using QRCodeAttendance.Presentation.Midllewares;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace QRCodeAttendance;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.Console(theme: AnsiConsoleTheme.Sixteen, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                 .WriteTo.File("logs/mylog.txt", rollingInterval: RollingInterval.Day)
                 .CreateLogger();
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddServices();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            app.UseCors("http");

            app.UseTokenMiddleware();
            app.UseExceptionHandlerMiddleware();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapGet("/", (HttpContext context) =>
            {
                context.Response.Redirect("/swagger");
                return;
            });

            app.MapControllers();

            app.Run();
        }
        catch (Exception e)
        {
            Log.Error(e.Message);
        }
    }
}