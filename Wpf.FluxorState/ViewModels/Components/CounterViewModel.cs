using Fluxor;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Wpf.FluxorState.Framework;
using Wpf.FluxorState.Framework.Abstractions;
using Wpf.FluxorState.Framework.Commands;
using Wpf.FluxorState.State.Features.Counter;

namespace Wpf.FluxorState.ViewModels.Components;

public class CounterViewModel : ViewModelBase
{
    private readonly IDispatcher _dispatcher;
    private readonly IDialogManager _dialogManager;
    private readonly IViewFactory _viewFactory;

    public CounterViewModel(
        IDispatcher dispatcher,
        IDialogManager dialogManager,
        IViewFactory viewFactory,
        IState<CounterState> _counterState)
    {
        _dispatcher = dispatcher;
        _dialogManager = dialogManager;
        _viewFactory = viewFactory;
        CounterState = _counterState;

        IncrementCount = new RelayCommand(OnIncrementCount);
        DecrementCount = new RelayCommand(OnDecrementCount);
        AppendChildView = new AsyncRelayCommand(OnAppendChildView);
    }

    public IState<CounterState> CounterState { get; set; } = default!;
    public IActionCommand IncrementCount { get; }
    public IActionCommand DecrementCount { get; }
    public IActionCommand AppendChildView { get; }

    private void OnIncrementCount()
    {
        _dispatcher.Dispatch(new IncrementCounterAction());
        OnPropertyChanged(nameof(CounterState));
    }

    private void OnDecrementCount()
    {
        _dispatcher.Dispatch(new DecrementCounterAction());
        OnPropertyChanged(nameof(CounterState));
    }

    private async Task OnAppendChildView()
    {
        ChildViews.Add(_viewFactory.CreateView<ChildComponentViewModel>());
        await Task.CompletedTask;
    }

    public ObservableCollection<UIElement> ChildViews { get; set; }
        = new ObservableCollection<UIElement>();
}
