using Microsoft.EntityFrameworkCore;
using ToDoAppApi.Data;
using ToDoAppApi.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyMethod()  
              .AllowAnyHeader();
    });
});


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); 
    options.ListenAnyIP(7116, listenOptions => listenOptions.UseHttps()); // HTTPS
});


builder.WebHost.UseUrls("http://localhost:5000");



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
