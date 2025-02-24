using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using Wpf.FluxorState.Store.Middlewares;

namespace Wpf.FluxorState.Store;

public static class FluxorConfiguration
{
    public static IServiceCollection AddFluxorStore(this IServiceCollection services)
    {
        services.AddFluxor(option =>
        {
            option.ScanAssemblies(typeof(FluxorConfiguration).Assembly);
        });
        services.AddScoped<LoggingMiddleware>();
        return services;
    }
}
