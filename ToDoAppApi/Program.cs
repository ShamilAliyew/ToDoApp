using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Data;
using ToDoAppApi.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS konfiqurasiyası
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Hər yerdən gələn istəklərə icazə verir
              .AllowAnyMethod()  // Hər hansı HTTP metodunu (GET, POST və s.) qəbul edir
              .AllowAnyHeader(); // Hər hansı başlıqla gələn istəklərə icazə verir
    });
});

// Kestrel server konfiqurasiyası
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(7116, listenOptions => listenOptions.UseHttps()); // HTTPS
});

// Add services to the container.
builder.WebHost.UseUrls("http://localhost:5000");

// Sessiya üçün konfiqurasiya
builder.Services.AddDistributedMemoryCache(); // Sessiya məlumatlarını yaddaşda saxlamaq üçün
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sessiyanın müddəti
    options.Cookie.HttpOnly = true;  // Cookie'yi yalnız serverdə oxumaq üçün
});

builder.Services.AddControllers();

// Swagger konfiqurasiyası
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MySQL bağlantısı
var connectionString = builder.Configuration.GetConnectionString("MysqlConnection");

builder.Services.AddDbContext<ToDoAppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40))));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Static files üçün konfiqurasiya
app.UseStaticFiles();

// Sessiya istifadəsi üçün əlavə
app.UseSession();  // Sessiyanı aktiv etmək üçün

// HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
