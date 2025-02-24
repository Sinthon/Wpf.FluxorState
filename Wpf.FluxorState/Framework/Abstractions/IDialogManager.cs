using System.Windows;

namespace Wpf.FluxorState.Framework.Abstractions;

public interface IDialogManager
{
    Task<TResult> ShowDialog<TResult>(DialogViewModel<TResult> dialogViewModel);
}
