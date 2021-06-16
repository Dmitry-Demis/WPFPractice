# WPFPractice
## v. 1.0.0
### EditNameViewModel
1. A converter from Visibility to Boolean has been replaced by **"BooleanToVisibilityConverter"**
2. Now you can see the text - "Field must not be empty" when there's no anything in a textbox. Otherwise, you can't see it
### MainWindowViewModel
1. A converter from Visibility to Boolean has been replaced by **"BooleanToVisibilityConverter"**


## *NOT READY* v. 1.0.1
### Parameter.cs
1. Adding comments
2. SelectedParameterType → ParameterType
### ChangeParameterViewModel.cs, ChangeParameterWindow.xaml
1. ChangeParameterViewModel.cs → EditValuesListViewModel
2. ChangeParameterWindow.xaml → EditValuesListWindow
### RelayCommand.cs, RelayCommandT.cs
1. public RelayCommand() → protected RelayCommand()
2. Adding CanExecute in an Execute function
### EditValuesListViewModel.cs
1. Adding dialogue sevrice for a message box
### EditNameViewModel.cs
1. Property Name. Adding OnPropertyChanged(nameof(IsNameEmpty));
2. IsNameEmpty →  get => string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name);
3. CloseCommand → !IsNameEmpty
