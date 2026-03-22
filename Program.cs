var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles(); // Для CSS/JS
app.UseRouting();

app.MapRazorPages(); // Маршрутизация страниц

app.Run();
