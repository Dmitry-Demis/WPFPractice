using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFPractice.Cmds;
using WPFPractice.Model;
using WPFPractice.View;

namespace WPFPractice.ViewModel
{
    class ChangeParameterViewModel : BaseViewModel, ICloseWindows
    {

        private readonly IDialogService dialogService;
        public ChangeParameterViewModel()
        {

        }
        public ChangeParameterViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        /// <summary>
        /// Values - наблюдаемая коллекция значений элементов
        /// </summary>
        public ObservableCollection<string> Values { get; set; } = new ObservableCollection<string>();
        /// <summary>
        /// CurrentItem - текущий выбранный элемент 
        /// </summary>
        public string CurrentItem { get; set; }

        /// <summary>
        /// AddItem - добавить строку в список
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
                           if (!string.IsNullOrEmpty(viewModel.Name))
                           {
                               CurrentItem = viewModel.Name;
                               Values.Add(CurrentItem);
                           }
                       }));
            }
        }

        /// <summary>
        /// DeleteItem - удалить строку из списка
        /// </summary>
        public RelayCommand _deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ??
                       (_deleteItem = new RelayCommand(() =>
                           {
                               if (CurrentItem == null)
                               {
                                   return;
                               }
                               string msg = $"Вы правда хотите удалить элемент {CurrentItem}?";
                               MessageBoxResult messageBoxResult = MessageBox.Show(msg, "Удаление элемента из таблицы",
                                   MessageBoxButton.YesNo, MessageBoxImage.Warning);
                               if (messageBoxResult == MessageBoxResult.Yes)
                               {
                                   Values.Remove(CurrentItem);
                               }
                               else
                               {
                                   return;
                               }
                           },
                           () =>
                           {
                               return Values.Count > 0;
                           }));
            }
        }

        /// <summary>
        /// ChangeItem - изменить текущий элемент
        /// </summary>
        public RelayCommand _changeItem;
        public RelayCommand ChangeItem
        {
            get
            {
                return _changeItem ??
                       (_changeItem = new RelayCommand(() =>
                           {
                               EditNameViewModel viewModel = new EditNameViewModel();
                               viewModel.Name = CurrentItem;
                               var index = Values.IndexOf(CurrentItem); 
                               if (!string.IsNullOrEmpty(viewModel.Name))
                               {
                                   CurrentItem = viewModel.Name;
                                   Values[index] = CurrentItem;
                               }
                           },
                           () =>
                           {
                               return Values.Count > 0;
                           }));
            }
        }

        /// <summary>
        /// UpCommand - переместить текущий элемент наверх
        /// </summary>
        private RelayCommand _upCommand;
        public RelayCommand UpCommand
        {
            get
            {
                return _upCommand ??
                       (_upCommand = new RelayCommand(() =>
                           {
                               var curr = CurrentItem;
                               var index1 = Values.IndexOf(curr);
                               var secondElement = Values.ElementAt(index1 - 1);
                               Values[index1] = secondElement;
                               Values[index1 - 1] = curr;
                               CurrentItem = curr;
                           },
                           () =>
                           {
                               return CurrentItem != null && CurrentItem != Values[0];
                           }));
            }
        }

        /// <summary>
        /// DownCommand - переместить текущий элемент вниз
        /// </summary>
        private RelayCommand _downCommand;
        public RelayCommand DownCommand
        {
            get
            {
                return _downCommand ??
                       (_downCommand = new RelayCommand(() =>
                           {
                               var curr = CurrentItem;
                               var index1 = Values.IndexOf(curr);
                               var secondElement = Values.ElementAt(index1 + 1);
                               Values[index1] = secondElement;
                               Values[index1 + 1] = curr;
                               CurrentItem = curr;
                           },
                           () =>
                           {
                               return CurrentItem != null && CurrentItem != Values[Values.Count - 1];
                           }));
            }
        }

        /// <summary>
        /// Команда для закрытия окна 
        /// </summary>
        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
            => _closeCommand ?? (_closeCommand = new RelayCommand(CloseWindow, () => Values.Count>0));

        public Action Close { get; set; }

        void CloseWindow()
        {
            Close?.Invoke();
        }
        /// <summary>
        /// Команда отмены
        /// </summary>
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand
            => _cancelCommand ?? (_cancelCommand = new RelayCommand(CloseWindow));
        public bool CanClose() => true;

    }
}
