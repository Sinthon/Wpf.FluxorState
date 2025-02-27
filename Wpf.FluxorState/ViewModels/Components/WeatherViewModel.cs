using Fluxor;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.Framework.Abstractions;
using Wpf.FluxorState.Framework.Commands;
using Wpf.FluxorState.Models;
using Wpf.FluxorState.State.Features.Weather;

namespace Wpf.FluxorState.ViewModels.Components;

public class WeatherViewModel : ViewModelBase, IDisposable
{
    private readonly IDispatcher _dispatcher;
    private readonly IState<WeatherState> _weatherState;

    public bool IsLoading => _weatherState.Value.IsLoading;
    public string ErrorMessage => _weatherState.Value.ErrorMessage;

    public ObservableCollection<WeatherForecast> Forecasts { get; } = new();

    public IActionCommand FetchWeatherCommand { get; }

    public WeatherViewModel(IDispatcher dispatcher, IState<WeatherState> weatherState)
    {
        _dispatcher = dispatcher;
        _weatherState = weatherState;

        FetchWeatherCommand = new AsyncRelayCommand(FetchWeather, () => !IsLoading);

        _weatherState.StateChanged += OnStateChanged;
        UpdateProperties();
    }

    private async Task FetchWeather()
    {
        _dispatcher.Dispatch(new FetchWeatherAction());
    }

    private void OnStateChanged(object? sender, EventArgs e) => UpdateProperties();

    private void UpdateProperties()
    {
        OnPropertyChanged(nameof(IsLoading));
        OnPropertyChanged(nameof(ErrorMessage));

        // Update Forecasts without reinitializing
        Forecasts.Clear();
        foreach (var forecast in _weatherState.Value.Forecasts)
            Forecasts.Add(forecast);

        // Notify CanExecuteChanged
        FetchWeatherCommand.RaiseCanExecuteChanged();
    }

    public void Dispose()
    {
        _weatherState.StateChanged -= OnStateChanged;
    }
}

