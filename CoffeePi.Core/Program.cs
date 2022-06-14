using CoffeePi.Core.Repositories;
using CoffeePi.Core.Services;
using CoffeePi.Data.Configuration;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Custom Services
builder.Services.AddScoped<IGpioService, GpioService>();

// Repositories
builder.Services.AddScoped<ISingleRoutineRepository, SingleRoutineRepository>();
builder.Services.AddScoped<IDailyRoutineRepository, DailyRoutineRepository>();
builder.Services.AddScoped<IWeeklyRoutineRepository, WeeklyRoutineRepository>();
builder.Services.AddScoped<IExecutedRoutineRepository, ExecutedRoutineRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database stuff
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CoffeePiContext>(options =>
    options.UseMySql(
        connectionString, 
        ServerVersion.AutoDetect(connectionString)
    )
);

WebApplication app = builder.Build();

// Migrate and create database
{
    using IServiceScope scope = app.Services.CreateScope();

    using CoffeePiContext context = scope.ServiceProvider.GetRequiredService<CoffeePiContext>();

    // Execute Migrations
    context.Database.Migrate();
}

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
