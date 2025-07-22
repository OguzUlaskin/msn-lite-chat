using MsnLiteChatApp.Hubs;
using MsnLiteChatApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(); // SignalR desteği
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Geliştirme ortamı için Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware sırası
app.UseHttpsRedirection();
app.UseStaticFiles();         // wwwroot klasörü erişimi için
app.UseDefaultFiles();        // index.html, login.html gibi dosyaları otomatik gösterir
app.UseRouting();             // ⬅ SignalR için routing burada olmalı
app.UseAuthorization();

// Ana sayfayı login.html’e yönlendir
app.MapGet("/", context =>
{
    context.Response.Redirect("/login.html");
    return Task.CompletedTask;
});

app.MapControllers();
app.MapHub<Chat
