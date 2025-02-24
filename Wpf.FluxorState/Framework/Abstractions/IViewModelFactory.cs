using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.FluxorState.Framework.Abstractions;

public interface IViewModelFactory
{
    TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
}