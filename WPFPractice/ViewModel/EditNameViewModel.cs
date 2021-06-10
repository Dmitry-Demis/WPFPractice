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
        private readonly IDialogService dialogService;
        public EditNameViewModel()
        {

        }
        public EditNameViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
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
            => _closeCommand ?? (_closeCommand = new RelayCommand(()=> Close?.Invoke(), ()=>!string.IsNullOrEmpty(Name)));
        public Action Close { get ; set ; }
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ?? (_cancelCommand =
            new RelayCommand(() => {
                Close?.Invoke();
                Name = null;
            }));
        public bool CanClose() => true;
        //TODO: не получается убрать надпись "Поле не должно быть пустым", не меняется 
        private bool _isNameEmpty;
        public bool IsNameEmpty
        {
            get
            {
                return (Name?.Length > 0);
            }
            private set
            {
                SetProperty(ref _isNameEmpty, value);
            }
        }
    }
    public class BoolToVisibilityEditNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
