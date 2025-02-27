using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Wpf.FluxorState.Framework.Abstractions;

namespace Wpf.FluxorState.Framework.Commands;

public class AsyncRelayCommand : IActionCommand
{
    private readonly Func<CancellationToken, Task>? _executeWithToken;
    private readonly Func<Task>? _executeWithoutToken;
    private readonly Func<bool>? _canExecute;
    private bool _isExecuting;

    public event EventHandler? CanExecuteChanged;

    // Constructor for methods WITH CancellationToken
    public AsyncRelayCommand(Func<CancellationToken, Task> execute, Func<bool>? canExecute = null)
    {
        _executeWithToken = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    // Constructor for methods WITHOUT CancellationToken
    public AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null)
    {
        _executeWithoutToken = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return !_isExecuting && (_canExecute?.Invoke() ?? true);
    }

    public async void Execute(object? parameter)
    {
        if (!CanExecute(parameter)) return;

        _isExecuting = true;
        RaiseCanExecuteChanged();

        try
        {
            if (_executeWithToken != null && parameter is CancellationToken token)
            {
                await _executeWithToken(token);
            }
            else if (_executeWithoutToken != null)
            {
                await _executeWithoutToken();
            }
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
