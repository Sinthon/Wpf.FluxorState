using Wpf.FluxorState.API.Weathers;

namespace Wpf.FluxorState.API.Persistence;

public static class WeatherContextExtensions
{
    private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public static void EnsureDatabaseCreated(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WeatherContext>();
        context.Database.EnsureCreated();
    }

    public static void SeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WeatherContext>();

        if (context.WeatherForecasts.Any())
            return;

        var weathers = Enumerable.Range(1, 500).Select(index => new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
        context.WeatherForecasts.AddRange(weathers);
        context.SaveChanges();
    }
}
