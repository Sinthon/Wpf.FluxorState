namespace Wpf.FluxorState.API.Common;

public class AgroogateRoot
{
    public Guid Id { get; set; }

    private List<Event> _events = new List<Event>();

    public void RecordEvent(Event @event)
    {
        _events.Add(@event);
    }

    public List<Event> GetEvents() => _events;

    public void ClearEvents() => _events.RemoveAll(x => x.Dispatched);
}
