using Microsoft.Extensions.DependencyInjection;

using Wpf.FluxorState.Framework.Abstractions;

namespace Wpf.FluxorState.Framework;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase
    {
        return ActivatorUtilities.CreateInstance<TViewModel>(_serviceProvider);
    }
}
