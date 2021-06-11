using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFPractice.Cmds;
using WPFPractice.Model;
using WPFPractice.View;

namespace WPFPractice.ViewModel
{

    public class EditNameViewModel : BaseViewModel, ICloseWindows
    {

        public Action Close { get; set; }
        public bool CanClose() => true;
        /// <summary>
        /// Name - a name of a parameter
        /// </summary>
        public string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                IsNameEmpty = (Name?.Length == 0);
            }
        }
        /// <summary>
        /// IsNameEmpty - shows a message if there's no anything in the textbox
        /// </summary>
        private bool _isNameEmpty = true;
        public bool IsNameEmpty
        {
            get => _isNameEmpty;
            set
            {
                SetProperty(ref _isNameEmpty, value);
            }
        }
        /// <summary>
        /// CloseCommand allows to close a window
        /// </summary>
        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
            => _closeCommand ?? (_closeCommand = new RelayCommand(() => Close?.Invoke(), () => !string.IsNullOrEmpty(Name)));

        /// <summary>
        /// CancelCommand allows to cancel input
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ?? (_cancelCommand =
            new RelayCommand(() => {
                Close?.Invoke();
                Name = null;
            }));

        /*private readonly IDialogService dialogService;
        public EditNameViewModel()
        {

        }
        public EditNameViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }        
        //TODO: [solved] не получается убрать надпись "Поле не должно быть пустым", не меняется
*/
    }   
}
