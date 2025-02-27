using Microsoft.Extensions.DependencyInjection;
using Refit;
using Wpf.FluxorState.Infrastructure.Services;

namespace Wpf.FluxorState.Infrastructure;

internal static class Configuration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRefitClient<IWeatherForecastService>()
        .ConfigureHttpClient(refitConfig =>
        {
            refitConfig.BaseAddress = new Uri("https://localhost:7141");
        });

        return services;
    }
}
