using Fluxor;
using Wpf.FluxorState.Infrastructure.Services;
using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.State.Features.Weather;

public class FetchWeatherEffect(IWeatherForecastService _weatherForecastService) : Effect<FetchWeatherAction>
{
    public override async Task HandleAsync(FetchWeatherAction action, IDispatcher dispatcher)
    {
        try
        {
            WeatherForecast[] forecasts = await _weatherForecastService.GetForecastAsync()
                ?? Array.Empty<WeatherForecast>();
            dispatcher.Dispatch(new FetchWeatherSuccessAction(forecasts));
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new FetchWeatherFailureAction(ex.Message));
        }
    }
}
