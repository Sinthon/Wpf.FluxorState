using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Wpf.FluxorState.Infrastructure.Services;
using Wpf.FluxorState.Infrastructure.SignalR;

namespace Wpf.FluxorState.Infrastructure;

internal static class Configuration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        /// Retifit
        services.AddRefitClient<IWeatherForecastService>()
            .ConfigureHttpClient(refitConfig =>
                refitConfig.BaseAddress = new Uri(config.GetConnectionString("API")!));

        /// SignalR
        services.AddSingleton<SignalRManager>();
        return services;
    }
}
