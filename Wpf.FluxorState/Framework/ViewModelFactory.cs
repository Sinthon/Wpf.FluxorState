using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.FluxorState.Framework.Abstractions;

namespace Wpf.FluxorState.Framework;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T CreateViewModel<T>() where T : class
    {
        return ActivatorUtilities.CreateInstance<T>(_serviceProvider);
    }

    public object CreateViewModel(Type viewModelType, params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance(_serviceProvider, viewModelType, parameters);
    }
}
