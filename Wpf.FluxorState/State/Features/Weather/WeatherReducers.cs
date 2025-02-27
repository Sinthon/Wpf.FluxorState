using Fluxor;
using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.State.Features.Weather;

public static class WeatherReducers
{
    [ReducerMethod]
    public static WeatherState ReduceFetchWeatherAction(WeatherState state, FetchWeatherAction action) =>
        state.With(isLoading: true, errorMessage: "", forecasts: Array.Empty<WeatherForecast>());

    [ReducerMethod]
    public static WeatherState ReduceFetchWeatherSuccessAction(WeatherState state, FetchWeatherSuccessAction action) =>
        state.With(isLoading: false, errorMessage: "", forecasts: action.Forecasts);

    [ReducerMethod]
    public static WeatherState ReduceFetchWeatherFailureAction(WeatherState state, FetchWeatherFailureAction action) =>
        state.With(isLoading: false, errorMessage: action.ErrorMessage, forecasts: Array.Empty<WeatherForecast>());
}
