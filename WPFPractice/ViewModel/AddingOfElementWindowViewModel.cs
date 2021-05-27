using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFPractice.Cmds;
using WPFPractice.Model;
using WPFPractice.View;

namespace WPFPractice.ViewModel
{

    public class AddingOfElementWindowViewModel : BaseViewModel, ICloseWindows, IDialogRequestClose
    {
        public string _name;
        public string Name 
        { 
            get => _name; 
            set
            {
                SetProperty(ref _name, value);
            } 
        }
        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
            => _closeCommand ?? (_closeCommand = new RelayCommand(CloseWindow, ()=>!string.IsNullOrEmpty(Name)));

        public Action Close { get ; set ; }

        void CloseWindow()
        {
            Close?.Invoke();
        }
        private RelayCommand _cancelCommand;

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public RelayCommand CancelCommand
            => _cancelCommand ?? (_cancelCommand = new RelayCommand(CloseWindow));
        public bool CanClose() => true;
    }
   
}
