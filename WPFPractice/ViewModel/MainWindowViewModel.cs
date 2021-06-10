using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using WPFPractice.Model;
using WPFPractice.Cmds;
using System.Windows.Controls.Primitives;
using WPFPractice.View;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using WPFPractice.BindingEnums;
using System.Windows.Input;

namespace WPFPractice.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IDialogService dialogService;

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;          
        }       
        public ObservableCollection<Parameter> Parameters { get; set; } = new ObservableCollection<Parameter>();
        private Parameter _currentParameter; 
        public Parameter CurrentParameter
        {
            get => _currentParameter;
            set
            {
                SetProperty(ref _currentParameter, value);
            }
        }        
        public bool IsEnabled { get; set; } = true;
        private RelayCommand _addItem;
        public RelayCommand AddItem
        {
            get
            {
                return _addItem ??
                  (_addItem = new RelayCommand( ()=>
                  {
                      EditNameViewModel viewModel = new EditNameViewModel();
                      dialogService.ShowDialog(viewModel);
                      if (!string.IsNullOrEmpty(viewModel.Name))
                      {
                         Parameter parameter = new Parameter();
                          parameter.Name = viewModel.Name;
                          parameter.SelectedParameterType = ParameterType.SimpleString;
                          parameter.Strings = new List<string>();
                          CurrentParameter = parameter;
                          Parameters.Add(CurrentParameter);
                          IsTableEmpty = false;                        
                      }
                  }));
            } 
        }
        //TODO: Сделать MessageBox в DialogService 
        public RelayCommand _deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ??
                       (_deleteItem = new RelayCommand(() =>
                           {
                               if (CurrentParameter==null)
                               {
                                   return;
                               }
                               string msg = $"Вы правда хотите удалить элемент {CurrentParameter.Name}?";
                               MessageBoxResult messageBoxResult = MessageBox.Show(msg, "Удаление элемента из таблицы", //Rem: а где использование DialogService'а
                                   MessageBoxButton.YesNo, MessageBoxImage.Warning);
                               if (messageBoxResult == MessageBoxResult.Yes)
                               {
                                   Parameters.Remove(CurrentParameter);
                               }
                               else
                               {
                                   return;
                               }
                               IsTableEmpty = (Parameters.Count == 0) ? true : false;


                           },
                           () =>
                           {
                               return Parameters.Count > 0;
                           }));
            }
        }

        private RelayCommand _upCommand;

        public RelayCommand UpCommand
        {
            get
            {
                return _upCommand ??
                       (_upCommand = new RelayCommand(() =>
                       {
                               var curr = CurrentParameter;
                               var index1 = Parameters.IndexOf(curr);
                               var secondElement = Parameters.ElementAt(index1 - 1);
                               Parameters[index1] = secondElement;
                               Parameters[index1 - 1] = curr;
                               CurrentParameter = curr;
                       },
                    () =>
                    {
                        return CurrentParameter!=null && CurrentParameter != Parameters?[0];
                    })); 
            }
        }

        private RelayCommand _downCommand;
        public RelayCommand DownCommand
        {
            get
            {
                return _downCommand ??
                       (_downCommand = new RelayCommand(() =>
                           {
                               var curr = CurrentParameter;
                               var index1 = Parameters.IndexOf(curr);
                               var secondElement = Parameters.ElementAt(index1 + 1);
                               Parameters[index1] = secondElement;
                               Parameters[index1 + 1] = curr;
                               CurrentParameter = curr;
                           },
                           () =>
                           {
                               return CurrentParameter != null && CurrentParameter != Parameters[Parameters.Count-1];
                           }));
            }
        }
        //TODO: Значения не сохраняются в окне
        private RelayCommand<Parameter> _changeParameterCommand;
        public RelayCommand<Parameter> ChangeParameterCommand
        {
            get
            {
                return _changeParameterCommand ??
                       (_changeParameterCommand = new RelayCommand<Parameter>((param) =>
                        {
                            //Rem: И здесь редактируем конкретный параметр
                            ChangeParameterViewModel viewModel = new ChangeParameterViewModel(dialogService);
                            dialogService.ShowDialog(viewModel);

                        },
                    (param) =>
                    {
                        if (param == null)
                        {
                            return false;
                        }
                        return param.SelectedParameterType == ParameterType.SetFromList || param.SelectedParameterType == ParameterType.ValueFromList;
                              
                    }));
            }           
        }
        
        private bool _isTableEmpty = true;
        public bool IsTableEmpty 
        {
            get
            {
                return (Parameters.Count == 0);
            }
            private set
            {
                SetProperty(ref _isTableEmpty, value);
            }           
        }
    }
    /// <summary>
    ///  There's a converter which a bool value replaces by Visibility 
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
