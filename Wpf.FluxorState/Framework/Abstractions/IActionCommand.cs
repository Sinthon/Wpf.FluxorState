using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf.FluxorState.Framework.Abstractions;

public interface IActionCommand : ICommand
{
    void RaiseCanExecuteChanged();
}
