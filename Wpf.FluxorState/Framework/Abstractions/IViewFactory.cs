using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf.FluxorState.Framework.Abstractions;

public interface IViewFactory
{
    UIElement CreateView<TViewModel>() where TViewModel : ViewModelBase;
}
