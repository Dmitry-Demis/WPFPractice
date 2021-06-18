using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPFPractice.Cmds;
using WPFPractice.Model;

namespace WPFPractice.ViewModel
{
    class EditValuesListViewModel : BaseViewModel, ICloseWindows
    {
        public event Action Closed;

        public void Close()
        {
            Closed?.Invoke();
        }

        /// <summary>
        /// DialogService for showing dialogues
        /// </summary>
        private readonly IDialogService dialogService;

        /// <summary>
        /// Current parameter
        /// </summary>
        private Parameter parameter;

        /// <summary>
        /// A constructor without parameters
        /// </summary>
        public EditValuesListViewModel(){}

        /// <summary>
        /// A constructor with parameters
        /// </summary>
        /// <param name="dialogService">using for showing dialogues</param>
        /// <param name="parameter">using for chosen parameter</param>
        public EditValuesListViewModel(IDialogService dialogService, Parameter parameter)
        {
            this.dialogService = dialogService;
            this.parameter = parameter;
            foreach (var value in parameter.ValuesList)
            {
                ValueList.Add(value);
            }
            CurrentItem = ValueList?.Count > 0 ? ValueList[0] : null;
        }

        /// <summary>
        /// An observable collection of a value list
        /// </summary>
        public ObservableCollection<string> ValueList { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Current item
        /// </summary>
        private string _currentItem;
        public string CurrentItem
        {
            get => _currentItem;
            set => SetProperty(ref _currentItem, value); 
        }

        /// <summary>
        /// A command of adding item to the list of values
        /// </summary>
        private RelayCommand _addItem;
        public RelayCommand AddItem
        {
            get
            {
                return _addItem ??
                       (_addItem = new RelayCommand(() =>
                       {
                           EditNameViewModel viewModel = new EditNameViewModel();
                           dialogService.ShowDialog(viewModel);
                           if (viewModel.Name!=null)
                           {
                               CurrentItem = viewModel.Name;                              
                               ValueList.Add(CurrentItem);
                           }
                       }));
            }
        }

        /// <summary>
        /// A command of deleting item from the list of values
        /// </summary>
        public RelayCommand _deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ??
                       (_deleteItem = new RelayCommand(() =>
                           {
                               if (dialogService.ShowMessageBoxDialog("Удаление элемента из таблицы", 
                                   $"Вы правда хотите удалить элемент {CurrentItem}?") == MessageBoxResult.Yes)
                                   ValueList.Remove(CurrentItem);
                           },
                        () =>
                        {
                            return CurrentItem!=null && ValueList?.Count > 0;
                        }));
            }
        }
       
        /// <summary>
        /// A command of changing item in the list of values
        /// </summary>
        public RelayCommand _changeItem;
        public RelayCommand ChangeItem
        {
            get
            {
                return _changeItem ??
                       (_changeItem = new RelayCommand(() =>
                           {
                               EditNameViewModel viewModel = new EditNameViewModel
                               {
                                   Name = CurrentItem
                               };
                               dialogService.ShowDialog(viewModel);
                               var index = ValueList.IndexOf(CurrentItem); 
                               if (viewModel.Name!=null)
                               {
                                   CurrentItem = viewModel.Name;
                                   ValueList[index] = CurrentItem;
                               }
                           },
                           () =>
                           {
                               return CurrentItem != null && ValueList?.Count > 0;
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
                               var currItem = CurrentItem;
                               var indexOfcurrItem = ValueList.IndexOf(currItem);
                               var nextElement = ValueList.ElementAt(indexOfcurrItem - 1);
                               ValueList[indexOfcurrItem] = nextElement;
                               ValueList[indexOfcurrItem - 1] = currItem;
                               CurrentItem = currItem;
                           },
                           () =>
                           {
                               return CurrentItem != null && CurrentItem != ValueList?[0];
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
                               var currItem = CurrentItem;
                               var indexOfcurrItem = ValueList.IndexOf(currItem);
                               var nextElement = ValueList.ElementAt(indexOfcurrItem + 1);
                               ValueList[indexOfcurrItem] = nextElement;
                               ValueList[indexOfcurrItem + 1] = currItem;
                               CurrentItem = currItem;                              
                           },
                           () =>
                           {
                               return CurrentItem != null && CurrentItem != ValueList?[ValueList.Count - 1];
                           }));
            }
        }

        /// <summary>
        /// A command, which closes the window
        /// </summary>
        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
            => _closeCommand ?? (_closeCommand = new RelayCommand(
                () =>
                {
                    parameter.ValuesList.Clear();
                    foreach (var item in ValueList)
                    {
                        parameter.ValuesList.Add(item);
                    }
                    Close();
                }, 
                () => ValueList?.Count>0));

        /// <summary>
        /// A command, which cancels changes
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ?? (_cancelCommand =
            new RelayCommand(() => {Close(); ValueList = null;}));        
    }
}
