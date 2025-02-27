using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.State.Features.Weather;

// Fetch
public record FetchWeatherAction;

// Fetch result
public record FetchWeatherSuccessAction(WeatherForecast[] Forecasts);
public record FetchWeatherFailureAction(string ErrorMessage);

// Events
public record WeatherCreatedAction(WeatherForecast Forecast);
public record WeatherUpdatedAction(WeatherForecast Forecast);
public record WeatherDeletedAction(WeatherForecast Forecast);
