using EfInheritance.Repository.DatabaseContext;
using EfInheritance.Repository.Helper;
using EfInheritance.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InheritanceContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddTransient<ICarService , CarHelper>();
builder.Services.AddTransient<IBikeServices , BikeHelper>();

Log.Logger = new LoggerConfiguration()
.WriteTo.Console()
.MinimumLevel.Information()
.CreateLogger();

builder.Host.UseSerilog();

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
