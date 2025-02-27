using Refit;
using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.Infrastructure.Services;

public interface IWeatherForecastService
{
    [Get("/api/weatherforecast")]
    Task<WeatherForecast[]> GetForecastAsync();
}
