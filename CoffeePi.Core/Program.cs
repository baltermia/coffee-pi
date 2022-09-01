using CoffeePi.Core.Repositories;
using CoffeePi.Core.Services;
using CoffeePi.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using CoffeePi.Shared.Converters;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Custom Services
builder.Services.AddScoped<IGpioService, GpioService>();
builder.Services.AddScoped<ISimulationService, SimulationService>(_ => new SimulationService("192.168.213.255", 1302));

// Repositories
builder.Services.AddScoped<ICoffeeRoutineRepository, CoffeeRoutineRepository>();
builder.Services.AddScoped<ISingleRoutineRepository, SingleRoutineRepository>();
builder.Services.AddScoped<IDailyRoutineRepository, DailyRoutineRepository>();
builder.Services.AddScoped<IWeeklyRoutineRepository, WeeklyRoutineRepository>();
builder.Services.AddScoped<IExecutedRoutineRepository, ExecutedRoutineRepository>();

// Routine Services
builder.Services.AddScoped<IRoutineService, RoutineService>();
builder.Services.AddSingleton<IPeriodicHostedService, PeriodicHostedService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<IPeriodicHostedService>());

// Add services to the container.
builder.Services
    .AddControllers(_ => TypeDescriptor.AddAttributes(typeof(TimeOnly), new TypeConverterAttribute(typeof(TimeOnlyTypeConverter))))
    .AddNewtonsoftJson(options => 
    {
        options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
        options.SerializerSettings.Converters.Add(new TimeOnlyJsonConverter());
    });
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

//Fix CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
}));

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
