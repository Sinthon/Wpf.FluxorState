using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.State.Features.Counter;

namespace Wpf.FluxorState.ViewModels.Components;

public class ChildComponentViewModel : ViewModelBase
{
    public ChildComponentViewModel(IState<CounterState> _counterState)
    {
        _counterState.StateChanged += (sender, e) => OnPropertyChanged(nameof(CounterState));
        CounterState = _counterState;
    }

    public IState<CounterState> CounterState { get; set; } = default!;
}
