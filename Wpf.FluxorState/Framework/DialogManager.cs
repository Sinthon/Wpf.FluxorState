using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.FluxorState.Framework.Abstractions;

namespace Wpf.FluxorState.Framework;

public class DialogManager : IDialogManager, IDisposable
{
    private readonly SemaphoreSlim _dialogLock = new SemaphoreSlim(1, 1);

    public async Task<TResult> ShowDialog<TResult>(DialogViewModel<TResult> dialogViewModel)
    {
        await _dialogLock.WaitAsync();
        try
        {
            throw new NotImplementedException();
        }
        finally
        {
            _dialogLock.Release();
        }
    }

    public void Dispose() => _dialogLock.Dispose();
}
