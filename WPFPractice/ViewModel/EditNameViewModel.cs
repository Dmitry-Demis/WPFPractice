using System;
using WPFPractice.Cmds;
using WPFPractice.Model;

namespace WPFPractice.ViewModel
{
    public class EditNameViewModel : BaseViewModel, ICloseWindows
    {
        public event Action Closed;
        public void Close()
        {
            Closed?.Invoke();
        }

        /// <summary>
        /// A name of a parameter
        /// </summary>
        public string _name;
        public string Name
        {
            get => _name;
            set
            {                
                SetProperty(ref _name, value);
                OnPropertyChanged(nameof(IsNameEmpty));                                      
            }
        }

        /// <summary>
        /// Shows a message if there's no anything in the textbox
        /// </summary>
        public bool IsNameEmpty
        {
            get => string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name);
        }

        /// <summary>
        /// Allows to close a window
        /// </summary>
        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
            => _closeCommand ?? 
            (_closeCommand = new RelayCommand(() => Close(), () => !IsNameEmpty));
       
        /// <summary>
        /// Allows to cancel input without saving
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ?? 
            (_cancelCommand = new RelayCommand(() => {Close(); Name = null; }));
    }   
}
