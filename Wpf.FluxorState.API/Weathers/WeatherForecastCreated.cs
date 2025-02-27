using Wpf.FluxorState.API.Common;

namespace Wpf.FluxorState.API.Weathers;

public class WeatherForecastCreated(WeatherForecast weather) : Event(weather);
