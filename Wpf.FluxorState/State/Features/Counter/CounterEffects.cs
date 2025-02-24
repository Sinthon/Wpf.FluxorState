using Fluxor;

namespace Wpf.FluxorState.State.Features.Counter;

public class CounterEffects
{
    [EffectMethod]
    public static Task HandleDecrementCounterAction(DecrementCounterAction action, IDispatcher dispatcher)
    {
        return Task.CompletedTask;
    }

    [EffectMethod]
    public static Task HandleIncrementCounterAction(IncrementCounterAction action, IDispatcher dispatcher)
    {
        return Task.CompletedTask;
    }
}
