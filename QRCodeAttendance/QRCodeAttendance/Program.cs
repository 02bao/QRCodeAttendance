using QRCodeAttendance.QRCodeAttendance;
using QRCodeAttendance.QRCodeAttendance.Presentation.Midllewares;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace QRCodeAttendance;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                 .WriteTo.File("logs/mylog.txt", rollingInterval: RollingInterval.Day)
                 .CreateLogger();


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

        app.MapControllers();

        app.Run();
    }
}