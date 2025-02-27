using Microsoft.EntityFrameworkCore;
using Wpf.FluxorState.API.Apis;
using Wpf.FluxorState.API.Hubs;
using Wpf.FluxorState.API.Persistence;
using Wpf.FluxorState.API.Weathers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PublishEventInterceptor>();
builder.Services.AddDbContext<WeatherContext>(options =>
{
    options.UseSqlite(builder.Configuration
        .GetConnectionString("Sqlite"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true);
    });
});

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.EnsureDatabaseCreated();
    app.SeedDatabase();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapWeatherForecastEndpoints();
app.UseCors();
app.MapHub<EventHub>("/eventhub");

app.Run();
