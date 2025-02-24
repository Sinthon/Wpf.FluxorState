using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.FluxorState.Framework;

public abstract class DialogViewModel<T> : ViewModelBase
{
    private readonly TaskCompletionSource<T> _closeTcs =
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    private T? _dialogResult;

    public T? DialogResult
    {
        get => _dialogResult;
        set
        {
            if (value != null)
            {
                _dialogResult = value;
                OnPropertyChanged();
            }
        }
    }

    protected void Close(T dialogResult)
    {
        DialogResult = dialogResult;
        _closeTcs.TrySetResult(dialogResult);
    }

    public async Task<T> WaitForCloseAsync() => await _closeTcs.Task;
}

public abstract class DialogViewModel : DialogViewModel<bool> { }
