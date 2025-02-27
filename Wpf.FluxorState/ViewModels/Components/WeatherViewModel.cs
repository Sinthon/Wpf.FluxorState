using Fluxor;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.Models;
using Wpf.FluxorState.State.Features.Weather;

namespace Wpf.FluxorState.ViewModels.Components;

public class WeatherViewModel : ViewModelBase
{
    private readonly IDispatcher _dispatcher;
    private readonly IState<WeatherState> _weatherState;

    public bool IsLoading => _weatherState.Value.IsLoading;
    public string ErrorMessage => _weatherState.Value.ErrorMessage;

    public ObservableCollection<WeatherForecast> Forecasts { get; } = new();

    public ICommand FetchWeatherCommand { get; }

    public WeatherViewModel(IDispatcher dispatcher, IState<WeatherState> weatherState)
    {
        _dispatcher = dispatcher;
        _weatherState = weatherState;

        UpdateProperties();

        // Subscribe to state changes
        _weatherState.StateChanged += (_, _) => UpdateProperties();

        FetchWeatherCommand = new RelayCommand(_ => FetchWeather());
    }

    private void FetchWeather() => _dispatcher.Dispatch(new FetchWeatherAction());

    private void UpdateProperties()
    {
        Forecasts.Clear();
        if(_weatherState.Value.Forecasts != null)
        {
            foreach (var forecast in _weatherState.Value.Forecasts)
            {
                Forecasts.Add(forecast);
            }
        }

        OnPropertyChanged(nameof(IsLoading));
        OnPropertyChanged(nameof(ErrorMessage));
        OnPropertyChanged(nameof(Forecasts));
    }
}
