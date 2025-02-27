using System;
using System.Windows;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.Framework.Abstractions;
using Wpf.FluxorState.Framework.Commands;
using Wpf.FluxorState.ViewModels.Components;

namespace Wpf.FluxorState.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private readonly IViewFactory _viewFactory;
    public MainWindowViewModel(IViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
        ShowCounter = new RelayCommand(OnShowCounter);
        ShowWeather = new RelayCommand(OnShowWeather);

        // Open counter page
        OnShowCounter();
    }

    public string Title { get; } = "Welcome to WPF with Fluxor!";

    public IActionCommand ShowCounter { get; }
    public IActionCommand ShowWeather { get; }

    public void OnShowCounter() => ActiveView = _viewFactory.CreateView<CounterViewModel>();

    public void OnShowWeather() => Application.Current.Dispatcher.Invoke(() => ActiveView = _viewFactory.CreateView<WeatherViewModel>());

    private UIElement? _active;
    public UIElement? ActiveView
    {
        get => _active;
        set
        {
            _active = value;
            OnPropertyChanged();
        }
    }
}

