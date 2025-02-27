using Wpf.FluxorState.API.Common;

namespace Wpf.FluxorState.API.Weathers;

public class WeatherForecast : AgroogateRoot
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
