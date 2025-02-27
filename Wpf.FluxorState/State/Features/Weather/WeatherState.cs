using Fluxor;
using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.State.Features.Weather;

[FeatureState]
public class WeatherState
{
    public bool IsLoading { get; } = false;
    public string ErrorMessage { get; } = string.Empty;
    public WeatherForecast[] Forecasts { get; } = Array.Empty<WeatherForecast>();

    public WeatherState() { }

    public WeatherState(bool isLoading = false, string errorMessage = "", WeatherForecast[]? forecasts = null)
    {
        IsLoading = isLoading;
        ErrorMessage = errorMessage;
        Forecasts = forecasts ?? Array.Empty<WeatherForecast>();
    }

    public WeatherState With(FetchWeatherAction action) =>
        new WeatherState(true, "", Array.Empty<WeatherForecast>());

    public WeatherState With(FetchWeatherSuccessAction action) =>
        new WeatherState(false, "", action.Forecasts);

    public WeatherState With(FetchWeatherFailureAction action) =>
        new WeatherState(false, action.ErrorMessage, Array.Empty<WeatherForecast>());

    public WeatherState With(WeatherCreatedAction action)
    {
        var forecasts = new[] { action.Forecast }
            .Concat(Forecasts)
            .ToArray();
        return new WeatherState(false, "", forecasts);
    }

    public WeatherState With(WeatherUpdatedAction action)
    {
        var forecasts = Forecasts
            .Where(x => x.Id != action.Forecast.Id)
            .Prepend(action.Forecast)
            .ToArray();
        return new WeatherState(false, "", forecasts);
    }

    public WeatherState With(WeatherDeletedAction action)
    {
        var forecasts = Forecasts
            .Where(x => x.Id != action.Forecast.Id)
            .ToArray();
        return new WeatherState(false, "", forecasts);
    }
}
