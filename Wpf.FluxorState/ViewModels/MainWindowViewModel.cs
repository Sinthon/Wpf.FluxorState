using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Input;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.Framework.Abstractions;
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

    public ICommand ShowCounter { get; }
    public ICommand ShowWeather { get; }

    public void OnShowCounter() => ActiveView = _viewFactory.CreateView<CounterViewModel>();

    public void OnShowWeather() => ActiveView = _viewFactory.CreateView<WeatherViewModel>();


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

    private string _title = "Welcome to WPF with Fluxor!";
    public string Title
    {
        get => _title;
        set
        {
            if (_title != value)
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}

