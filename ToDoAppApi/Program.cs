using Microsoft.EntityFrameworkCore;
using System;
using ToDoAppApi.Data;
using ToDoAppApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // H?r yerd?n g?l?n ist?kl?r? icaz? verir
              .AllowAnyMethod()  // H?r hans? HTTP metodunu (GET, POST v? s.) q?bul edir
              .AllowAnyHeader(); // H?r hans? ba?l?qla g?l?n ist?kl?r? icaz? verir
    });
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(7116, listenOptions => listenOptions.UseHttps()); // HTTPS
});
// Add services to the container.
builder.WebHost.UseUrls("http://localhost:5000");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("MysqlConnection");


builder.Services.AddDbContext<ToDoAppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 40))));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITodoService, TodoService>();



var app = builder.Build();
app.UseStaticFiles();  
app.MapFallbackToFile("index.html"); 

// Configure the HTTP request pipeline.
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
