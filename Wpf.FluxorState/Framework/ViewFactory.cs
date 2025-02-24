using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Wpf.FluxorState.Framework.Abstractions;
using Wpf.FluxorState.ViewModels;
using Wpf.FluxorState.ViewModels.Components;
using Wpf.FluxorState.ViewModels.Dialogs;
using Wpf.FluxorState.Views;
using Wpf.FluxorState.Views.Components;
using Wpf.FluxorState.Views.Dialogs;

namespace Wpf.FluxorState.Framework;

internal class ViewFactory : IViewFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IViewModelFactory _vmFactory;

    public ViewFactory(IServiceProvider serviceProvider, IViewModelFactory vmFactory)
    {
        _serviceProvider = serviceProvider;
        _vmFactory = vmFactory;
    }

    public UIElement CreateView<TViewModel>() where TViewModel : ViewModelBase => BindView(_vmFactory.CreateViewModel<TViewModel>());

    private UIElement BindView(ViewModelBase viewModel)
    {
        return viewModel switch
        {
            MainWindowViewModel vm => CreateViewInstance(typeof(MainWindow), vm),
            ChildComponentViewModel vm => CreateViewInstance(typeof(ChildComponentView), vm),
            CounterViewModel vm => CreateViewInstance(typeof(CounterView), vm),
            DialogViewModel vm => CreateViewInstance(typeof(DialogView), vm),
            _ => throw new InvalidOperationException($"No view registered for ViewModel {viewModel.GetType().Name}")
        };
    }

    private UIElement CreateViewInstance(Type viewType, ViewModelBase viewModel)
    {
        if (ActivatorUtilities.CreateInstance(_serviceProvider, viewType) is not FrameworkElement viewInstance)
            throw new InvalidOperationException();

        viewInstance.DataContext = viewModel;
        return viewInstance;
    }
}

