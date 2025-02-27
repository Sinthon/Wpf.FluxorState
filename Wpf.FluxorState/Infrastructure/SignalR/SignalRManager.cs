using Fluxor;
using Wpf.FluxorState.Models;
using Wpf.FluxorState.State.Features.Notification;
using Wpf.FluxorState.State.Features.Weather;
using Microsoft.AspNetCore.SignalR.Client;

namespace Wpf.FluxorState.Infrastructure.SignalR;

public class SignalRManager : IDisposable
{
    private readonly IDispatcher _dispatcher;
    private HubConnection? _hubConnection;

    public SignalRManager(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public void Dispose()
    {
        if (_hubConnection != null)
        {
            _hubConnection.StopAsync()
                .GetAwaiter();
        }
    }

    public async Task StartConnectionAsync(string hubUrl)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<WeatherForecast>("WeatherForecastDeleted", forecast =>
        {
            _dispatcher.Dispatch(new WeatherDeletedAction(forecast));
        });

        _hubConnection.On<WeatherForecast>("WeatherForecastCreated", forecast =>
        {
            _dispatcher.Dispatch(new WeatherCreatedAction(forecast));
        });

        _hubConnection.On<WeatherForecast>("WeatherForecastUpdated", forecast =>
        {
            _dispatcher.Dispatch(new WeatherUpdatedAction(forecast));
        });

        _hubConnection.On<string>("NewNotification", message =>
        {
            _dispatcher.Dispatch(new NotificationReceivedAction(message));
        });

        await _hubConnection.StartAsync();
    }
}
