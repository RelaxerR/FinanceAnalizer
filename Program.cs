using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 1. Настройка Serilog
const string logTemplate = "[{Level:w4}] [{Timestamp:yyyy-MM-dd HH:mm:ss}] [{SourceContext}]: {Message:lj}{NewLine}{Exception}";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    // Настройка консоли
    .WriteTo.Console(outputTemplate: logTemplate)
    // Настройка файла
    .WriteTo.File("logs/log-.txt", 
        rollingInterval: RollingInterval.Day, 
        outputTemplate: logTemplate)
    .CreateLogger();

// Указываем .NET использовать Serilog
builder.Host.UseSerilog();

builder.Services.AddRazorPages();

var app = builder.Build();

// Логируем первый запуск
Log.Information("Приложение запущено и готово к работе");

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

try 
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Приложение аварийно завершилось");
}
finally
{
    Log.CloseAndFlush();
}
