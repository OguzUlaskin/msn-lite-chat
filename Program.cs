using MsnLiteChatApp.Hubs;
using MsnLiteChatApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();


// Veritabanı bağlantısını buraya ekliyoruz:
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ← BU SATIR OLMALI
app.UseDefaultFiles(); // wwwroot/index.html, login.html gibi dosyaları tanır
app.UseAuthorization();
app.UseRouting();

// 🔽 BU KISMIN HEMEN ALTINA EKLE ⬇
app.MapGet("/", context =>
{
    context.Response.Redirect("/login.html");
    return Task.CompletedTask;
});

app.MapControllers();
app.MapHub<ChatHub>("/chathub");


app.Run();
