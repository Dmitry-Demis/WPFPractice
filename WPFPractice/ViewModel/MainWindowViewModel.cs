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

namespace WPFPractice.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
      
        public ObservableCollection<Parameter> Parameters { get; set; } = new ObservableCollection<Parameter>();       
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private Parameter _currentItem; 

        public Parameter CurrentItem
        {
            get => _currentItem;
            set
            {
               /* if (_currentItem == value)
                {
                    return;
                }*/
                _currentItem = value;
                OnPropertyChanged(nameof(CurrentItem));
            }
        }

        public DataType DefaultItem { get; set; } = DataType.SetFromList;
        public DataType TypesDataType { get; set; }

        
        public string ButtonValueType { get; set; } = "Список...";
        public bool IsEnabled { get; set; } = true;
        private RelayCommand _addItem;
        public RelayCommand AddItem
        {
            get
            {
                return _addItem ??
                  (_addItem = new RelayCommand( ()=>
                  {
                      AddingOfElementWindowViewModel viewModel = new AddingOfElementWindowViewModel();
                      AddingOfElementWindow window = new AddingOfElementWindow { DataContext = viewModel };
                      window.ShowDialog();
                      if (!string.IsNullOrEmpty(viewModel.Name))
                      {
                         Parameter parameter = new Parameter();
                          //parameter.Name = viewModel.Name;
                          //Parameters.Add(parameter);
                          parameter.Name = viewModel.Name;
                          parameter.Types = new DataType();
                          parameter.Strings = new List<string>();
                          CurrentItem = parameter;
                          Parameters.Add(CurrentItem);
                         
                      }
                  }));
            } 
        }

        public RelayCommand _deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ??
                       (_deleteItem = new RelayCommand(() =>
                           {
                               if (CurrentItem==null)
                               {
                                   return;
                               }
                               string msg = $"Вы правда хотите удалить элемент {CurrentItem.Name}?";
                               MessageBoxResult messageBoxResult = MessageBox.Show(msg, "Удаление элемента из таблицы",
                                   MessageBoxButton.YesNo, MessageBoxImage.Warning);
                               if (messageBoxResult == MessageBoxResult.Yes)
                               {
                                   Parameters.Remove(CurrentItem);
                               }
                               else
                               {
                                   return;
                               }
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
                               var curr = CurrentItem;
                               var index1 = Parameters.IndexOf(curr);
                               var secondElement = Parameters.ElementAt(index1 - 1);
                               Parameters[index1] = secondElement;
                               Parameters[index1 - 1] = curr;
                               CurrentItem = curr;
                           },
                           () =>
                           {
                               return CurrentItem!=null && CurrentItem != Parameters[0];
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
                               var curr = CurrentItem;
                               var index1 = Parameters.IndexOf(curr);
                               var secondElement = Parameters.ElementAt(index1 + 1);
                               Parameters[index1] = secondElement;
                               Parameters[index1 + 1] = curr;
                               CurrentItem = curr;
                           },
                           () =>
                           {
                               return CurrentItem != null && CurrentItem != Parameters[Parameters.Count-1];
                           }));
            }
        }

        //TODO: Реализовать команду открытия нового окна на основе значений DataGrid [Сложности с привязкой к различным данным]

        private RelayCommand _windowOfParameters;
        public RelayCommand WindowOfParameters
        {
            get
            {
                return _windowOfParameters ??
                       (_windowOfParameters = new RelayCommand(() =>
                           {

                               ListOfDataViewModel viewModel = new ListOfDataViewModel();
                               ListOfData window = new ListOfData { DataContext = viewModel };
                               window.ShowDialog();

                           },
                           () =>
                           {
                               //var curr = CurrentItem;
                               //if (curr == null) return false;
                               //if (curr.Types==DataType.SetFromList || curr.Types == DataType.ValueFromList)
                               //{
                               //    IsEnabled = true;
                               //}
                               return IsEnabled;
                           }));
            }
           
        }

    }

    /// <summary>
    /// Конвертер, который преобразует enum в строки для отображения
    /// </summary>
    public class ConvertEnumToString : IValueConverter
    {
        public object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            if (value != null)
            {
                var dataType = (DataType)value;
                string s;
                switch (dataType)
                {
                    case DataType.SimpleString:
                        s =  "Простая строка";
                        break;
                    case DataType.StringWithHistory:
                        s = "Строка с историей";
                        break;
                    case DataType.ValueFromList:
                        s = "Значение из списка";
                        break;
                    case DataType.SetFromList:
                        s = "Набор значений из списка";
                        break;
                    default:
                        s = null;
                        break;
                }

                return s;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
