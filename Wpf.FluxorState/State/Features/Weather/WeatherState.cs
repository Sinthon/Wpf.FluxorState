using Fluxor;
using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.State.Features.Weather;

[FeatureState]
public class WeatherState
{
    public bool IsLoading { get; } = false;
    public string ErrorMessage { get; } = string.Empty;
    public WeatherForecast[] Forecasts { get; } = [];

    public WeatherState() { }

    public WeatherState(bool isLoading = false, string errorMessage = "", WeatherForecast[]? forecasts = null)
    {
        IsLoading = isLoading;
        ErrorMessage = errorMessage;
        Forecasts = forecasts ?? Array.Empty<WeatherForecast>();
    }

    public WeatherState With(bool? isLoading = null, string? errorMessage = null, WeatherForecast[]? forecasts = null) =>
        new WeatherState(
            isLoading ?? IsLoading,
            errorMessage ?? ErrorMessage,
            forecasts ?? Forecasts);
}

