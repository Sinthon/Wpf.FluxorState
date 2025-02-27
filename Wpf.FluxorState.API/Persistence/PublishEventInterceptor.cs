using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Wpf.FluxorState.API.Common;
using Wpf.FluxorState.API.Hubs;

namespace Wpf.FluxorState.API.Persistence;

public class PublishEventInterceptor(IHubContext<EventHub> eventHub) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var events = ExractEvents(eventData.Context);

        var savedChangeResult = base.SavingChanges(
            eventData,
            result);

        DispatchEvents(events)
            .GetAwaiter()
            .GetResult();

        return savedChangeResult;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var events = ExractEvents(eventData.Context);

        var savedChangeResutl = await base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);

        await DispatchEvents(events);
        return savedChangeResutl;
    }

    private List<Event> ExractEvents(DbContext? context)
    {
        if (context is null)
            return new List<Event>();

        var entries = context.ChangeTracker
            .Entries<AgroogateRoot>();

        var events = entries
            .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            .SelectMany(x => x.Entity.GetEvents())
            .ToList();

        return events;
    }

    private async Task DispatchEvents(List<Event> events)
    {
        foreach (var @event in events)
        {
            if (@event.Dispatched)
                continue;

            await eventHub.Clients.All.SendAsync(@event.GetType().Name, @event.Body);
            @event.MarkAsDispatched();
        }
    }

    private void ClearDispatchedEvents(DbContextEventData eventData)
    {
        var entries = eventData.Context!
            .ChangeTracker
            .Entries<AgroogateRoot>();

        foreach (var entry in entries)
            entry.Entity.ClearEvents();
    }
}
