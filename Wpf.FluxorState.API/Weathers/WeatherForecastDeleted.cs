using Wpf.FluxorState.API.Common;

namespace Wpf.FluxorState.API.Weathers;

public class WeatherForecastDeleted(WeatherForecast weather) : Event(weather);
