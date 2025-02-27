using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.State.Features.Weather;

public record FetchWeatherAction;
public record FetchWeatherSuccessAction(WeatherForecast[] Forecasts);
public record FetchWeatherFailureAction(string ErrorMessage);
