using System.Collections.ObjectModel;
using System.Linq;
using WPFPractice.Model;
using WPFPractice.Cmds;
using System.Windows;
using WPFPractice.BindingEnums;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using System;
using WPFPractice.Interfaces;

namespace WPFPractice.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        /// <summary>
        /// Services
        /// </summary>
        private readonly IDialogService dialogService;
        private readonly IFileService fileService;

        public MainWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }       

        /// <summary>
        /// Parameters - a collection of parametres in a data table
        /// </summary>
        public ObservableCollection<Parameter> Parameters { get; set; } = new ObservableCollection<Parameter>();

        /// <summary>
        /// Name - a name of a parameter
        /// </summary>
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }

        /// <summary>
        /// CurrentParameter - a selected parameter in a datatable shows here
        /// </summary>
        private Parameter _currentParameter; 
        public Parameter CurrentParameter
        {
            get => _currentParameter;
            set
            {
                SetProperty(ref _currentParameter, value);
            }
        }

        /// <summary>
        /// A command of adding item to the list of parameters
        /// </summary>
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
                      if (viewModel.Name!=null)
                      {
                          Parameter parameter = new Parameter();
                          parameter.Name = viewModel.Name;
                          parameter.ParameterType = ParameterType.SimpleString;
                          parameter.ValuesList = new ObservableCollection<string>();
                          CurrentParameter = parameter;
                          Parameters.Add(CurrentParameter);
                          OnPropertyChanged(nameof(IsTableEmpty));
                      }
                  }));
            } 
        }

        /// <summary>
        /// A command of deleting item from the list of parameters
        /// </summary>
        public RelayCommand _deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ??
                       (_deleteItem = new RelayCommand(() =>
                           {
                               if (dialogService.ShowMessageBoxDialog("Удаление элемента из таблицы", $"Вы правда хотите удалить элемент {CurrentParameter.Name}?") == MessageBoxResult.Yes)
                                   Parameters.Remove(CurrentParameter);
                           },
                           () =>
                           {
                               return CurrentParameter != null && Parameters.Count > 0;
                           }));
            }
        }

        /// <summary>
        /// A command, which raises the element 
        /// </summary>
        private RelayCommand _upCommand;
        public RelayCommand UpCommand
        {
            get
            {
                return _upCommand ??
                       (_upCommand = new RelayCommand(() =>
                       {
                           var currParameter = CurrentParameter;
                           var indexOfcurrItem = Parameters.IndexOf(currParameter);
                           var nextElement = Parameters.ElementAt(indexOfcurrItem - 1);
                           Parameters[indexOfcurrItem] = nextElement;
                           Parameters[indexOfcurrItem - 1] = currParameter;
                           CurrentParameter = currParameter;
                       },
                           () =>
                           {
                               return CurrentParameter != null && CurrentParameter != Parameters?[0];
                           }));
            }
        }
        /// <summary>
        /// A command, which lowers the element down
        /// </summary>
        private RelayCommand _downCommand;
        public RelayCommand DownCommand
        {
            get
            {
                return _downCommand ??
                       (_downCommand = new RelayCommand(() =>
                           {
                               var currParameter = CurrentParameter;
                               var indexOfcurrItem = Parameters.IndexOf(currParameter);
                               var nextElement = Parameters.ElementAt(indexOfcurrItem + 1);
                               Parameters[indexOfcurrItem] = nextElement;
                               Parameters[indexOfcurrItem + 1] = currParameter;
                               CurrentParameter = currParameter;
                           },
                           () =>
                           {
                               return CurrentParameter != null && CurrentParameter != Parameters[Parameters.Count-1];
                           }));
            }
        }

        /// <summary>
        /// A command, which can change a selected parameter
        /// </summary>
        private RelayCommand<Parameter> _changeParameterCommand;
        public RelayCommand<Parameter> ChangeParameterCommand
        {
            get
            {
                return _changeParameterCommand ??
                       (_changeParameterCommand = new RelayCommand<Parameter>((param) =>
                        {
                            EditValuesListViewModel viewModel = new EditValuesListViewModel(dialogService, param);
                            dialogService.ShowDialog(viewModel);
                        },
                    (param) =>
                    {
                        return param?.ParameterType == ParameterType.SetFromList || param?.ParameterType == ParameterType.ValueFromList;
                              
                    }));
            }           
        }

        /// <summary>
        /// Checking if the table empty or not
        /// </summary>
        public bool IsTableEmpty => Parameters.Count == 0;

        /// <summary>
        /// A command, which allows to save a datatable
        /// </summary>
        private RelayCommand _saveFileDialogCommand;
        public RelayCommand SaveFileDialogCommand 
        { 
            get
            {
                return _saveFileDialogCommand ??
                       (_saveFileDialogCommand = new RelayCommand(() =>
                       {
                           try
                           {
                               if (dialogService.SaveFileDialog()==true)
                               {
                                   fileService.Save(dialogService.FilePath, Parameters.ToList());
                                   dialogService.ShowMessageBoxDialog("Файл сохранен");
                               }
                           }
                           catch (Exception ex)
                           {
                               dialogService.ShowMessageBoxDialog(ex.Message);
                           }                          
                       }));
            }
        }
        private RelayCommand<Parameter> _openFileDialogCommand;
        public RelayCommand<Parameter> OpenFileDialogCommand
        {
            get
            {
                return _openFileDialogCommand ??
                  (_openFileDialogCommand = new RelayCommand<Parameter>((param) =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var parameters = fileService.Open(dialogService.FilePath);
                              Parameters.Clear();
                              foreach (var p in parameters)
                                  Parameters.Add(p);
                              dialogService.ShowMessageBoxDialog("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessageBoxDialog(ex.Message);
                      }
                  }));
            }
        }
    }

}

