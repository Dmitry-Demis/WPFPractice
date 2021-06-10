using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.Model
{
    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : INotifyPropertyChanged
                                           where TView : IDialog;
        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : INotifyPropertyChanged;
        bool? ShowMessageBoxDialog<TViewModel>(TViewModel viewModel) where TViewModel : INotifyPropertyChanged;
    }
}
