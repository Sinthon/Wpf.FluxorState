using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.FluxorState.Framework;

namespace Wpf.FluxorState.State.Features.Counter;

[FeatureState]
public class CounterState
{
    public int ClickCount { get; set; }

    public CounterState(int clickCount)
    {
        ClickCount = clickCount;
    }

    public CounterState() { }
}
