using AccessPoint.Application.Interfaces;
using AccessPoint.Application.Services;
using AccessPoint.Application.Services.Implementation;
using AccessPoint.Application.Services.Interfaces;
using AccessPoint.Infrastructure;
using AccessPoint.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the secret key from appsettings.json
var jwtSecret = builder.Configuration["JwtSettings:SecretKey"];
// Configure ApplicationDbContext with SQL Server connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration of serilog for text file logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs/AccessPoint-Logs.txt", outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Register all the required services here
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginHistoryService, LoginHistoryService>();
builder.Services.AddSingleton(new JwtService(jwtSecret));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();