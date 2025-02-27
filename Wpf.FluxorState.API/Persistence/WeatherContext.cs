using Microsoft.EntityFrameworkCore;
using Wpf.FluxorState.API.Weathers;

namespace Wpf.FluxorState.API.Persistence;

public class WeatherContext(DbContextOptions<WeatherContext> options, PublishEventInterceptor publishEvent) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(publishEvent);
        optionsBuilder.AddInterceptors(publishEvent);
    }
}
