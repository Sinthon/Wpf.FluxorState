using Microsoft.AspNetCore.SignalR;

namespace Wpf.FluxorState.API.Hubs;

public class EventHub(ILogger<EventHub> logger) : Hub
{
    public override Task OnConnectedAsync()
    {
        logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task BroadcastEvent(string eventName, object eventData)
    {
        await Clients.All.SendAsync(eventName, eventData);
    }
}
