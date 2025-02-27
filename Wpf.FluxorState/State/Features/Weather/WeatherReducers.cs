using Fluxor;
using Wpf.FluxorState.Models;

namespace Wpf.FluxorState.State.Features.Weather;

public static class WeatherReducers
{
    // Fetch 
    [ReducerMethod]
    public static WeatherState ReduceFetchWeatherAction(WeatherState state, FetchWeatherAction action) =>
        state.With(action);

    [ReducerMethod]
    public static WeatherState ReduceFetchWeatherSuccessAction(WeatherState state, FetchWeatherSuccessAction action) =>
        state.With(action);

    [ReducerMethod]
    public static WeatherState ReduceFetchWeatherFailureAction(WeatherState state, FetchWeatherFailureAction action) =>
        state.With(action);

    // Events
    [ReducerMethod]
    public static WeatherState ReduceWeatherCreatedActionAction(WeatherState state, WeatherCreatedAction action) =>
        state.With(action);

    [ReducerMethod]
    public static WeatherState ReduceWeatherUpdatedActionAction(WeatherState state, WeatherUpdatedAction action) =>
        state.With(action);

    [ReducerMethod]
    public static WeatherState ReduceWeatherDeletedActionAction(WeatherState state, WeatherDeletedAction action) =>
        state.With(action);
}
