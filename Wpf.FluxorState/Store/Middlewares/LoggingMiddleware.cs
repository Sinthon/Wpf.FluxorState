using Fluxor;
using System;
using System.Threading.Tasks;

namespace Wpf.FluxorState.Store.Middlewares;

public class LoggingMiddleware : Middleware
{
    private IStore? _store;

    public override Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        _store = store;
        Console.WriteLine("LoggingMiddleware initialized.");
        return Task.CompletedTask;
    }

    public override bool MayDispatchAction(object action)
    {
        Console.WriteLine($"Dispatching Action: {action.GetType().Name}");
        return true; // Allow all actions to be dispatched
    }

    public override void BeforeDispatch(object action)
    {
        Console.WriteLine($"Before Dispatch: {action.GetType().Name}");
    }

    public override void AfterDispatch(object action)
    {
        Console.WriteLine($"After Dispatch: {action.GetType().Name}");
    }
}

