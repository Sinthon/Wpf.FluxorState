using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Wpf.FluxorState.API.Persistence;

namespace Wpf.FluxorState.API.Weathers;

public class WeatherService(WeatherContext context, ILogger<WeatherService> logger, CancellationToken ctn)
{
    public WeatherContext Context { get; } = context;
    public ILogger<WeatherService> Logger { get; } = logger;
    public CancellationToken Ctn { get; } = ctn;
}
