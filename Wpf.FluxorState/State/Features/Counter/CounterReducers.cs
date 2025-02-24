using Fluxor;

namespace Wpf.FluxorState.State.Features.Counter;

public class IncrementCounterReducer : Reducer<CounterState, IncrementCounterAction>
{
    public override CounterState Reduce(CounterState state, IncrementCounterAction action)
    {
        return new(clickCount: state.ClickCount + 1);
    }
}

public class DecrementCounterReducer : Reducer<CounterState, DecrementCounterAction>
{
    public override CounterState Reduce(CounterState state, DecrementCounterAction action)
    {
        return new(clickCount: state.ClickCount - 1);
    }
}

//public class T
//{
//    [ReducerMethod]
//    public static CounterState ReduceIncrementCounterAction(CounterState state, IncrementCounterAction action) =>
//        new(clickCount: state.ClickCount + 1);

//    [ReducerMethod]
//    public static CounterState ReduceDecrementCounterAction(CounterState state, DecrementCounterAction action) =>
//        new(clickCount: state.ClickCount - 1);
//}

