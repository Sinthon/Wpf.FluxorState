using System.Security.Cryptography.X509Certificates;

namespace Wpf.FluxorState.API.Common;

public class Event
{
    public Event(object? body)
    {
        Body = body;
    }

    public Event() { }

    public object? Body { get; private set; }

    public bool Dispatched { get; private set; }

    public void MarkAsDispatched()
    {
        Dispatched = true;
    }
}
