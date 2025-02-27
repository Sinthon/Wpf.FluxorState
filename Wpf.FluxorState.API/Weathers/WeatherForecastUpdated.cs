using Wpf.FluxorState.API.Common;

namespace Wpf.FluxorState.API.Weathers;

public class WeatherForecastUpdated(WeatherForecast weather) : Event(weather);
