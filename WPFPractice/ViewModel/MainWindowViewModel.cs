using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using WPFPractice.Model;
using WPFPractice.Cmds;
using System.Windows.Controls.Primitives;
using WPFPractice.View;

namespace WPFPractice.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        IFileService fileService = new JsonFileService();
        IDialogService dialogService = new DefaultDialogService();

        private RelayCommand<Parametres> _saveCommand;
        public RelayCommand<Parametres> SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand<Parametres>(obj =>
                {
                    try
                    {
                        if (dialogService.SaveFileDialog() == true)
                        {
                            fileService.Save(dialogService.FilePath, AllParametres.ToList());
                            dialogService.ShowMessage("Файл сохранен");
                        }
                    }
                    catch (Exception ex)
                    {
                        dialogService.ShowMessage(ex.Message);
                    }
                }));
            }
        }
        private RelayCommand<Parametres> _openCommand;
        public RelayCommand<Parametres> OpenCommand
        {
            get
            {
                return _openCommand ??
                  (_openCommand = new RelayCommand<Parametres>(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var phones = fileService.Open(dialogService.FilePath);
                              AllParametres.Clear();
                              foreach (var p in phones)
                                  AllParametres.Add(p);
                              dialogService.ShowMessage("Файл открыт");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        public MainWindowViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
        }
        private RelayCommand _openAnotherWindow;
        public RelayCommand OpenAnotherWindow
        {
            get
            {
                return _openAnotherWindow ??
                    (
                    _openAnotherWindow = new RelayCommand(()=>
                    {
                        ListOfData listOfData = new ListOfData();
                        listOfData.Show();
                    }
                    ));
            }
        }







        private Parametres _currentParametres;
        private string _selectedType;
        public string SelectedType {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }
        public ObservableCollection<string> _types;
        public ObservableCollection<string> Types
        {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
            }
        }
        public MainWindowViewModel()
        {
            _types = new ObservableCollection<string>();
            _types.Add("Простая строка");
            _types.Add("Строка с историей");
            _types.Add("Значение из списка");
            _types.Add("Набор значений из списка");
        }
        public Parametres CurrentParametres
        {
            get
            {
                if (_currentParametres==null)
                {
                    _currentParametres = new Parametres();
                }
                return _currentParametres; 
            }
            set
            {
                _currentParametres = value;
                OnPropertyChanged(nameof(CurrentParametres));
            }
        }
        ObservableCollection<Parametres> _allParametres;
        public ObservableCollection<Parametres> AllParametres
        {
            get
            {
                if (_allParametres == null)
                {
                    _allParametres = ParametresRepository.AllClients;
                }
                return _allParametres;
            }
        }
        //adding a new string to the table
        private RelayCommand<Parametres> addCommand;
        private RelayCommand<Parametres> deleteCommand;
        public RelayCommand<Parametres> AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = 
                    new RelayCommand<Parametres>
                    (obj=>
                        {
                            Parametres parametres = new Parametres();
                            AllParametres.Insert(AllParametres.Count, parametres);
                            SelectedParametres = parametres;
                            ChangeId();
                            parametres.NameOfParametre = $"Параметр {parametres.Id}";
                            OnPropertyChanged(nameof(AddCommand));
                            OnPropertyChanged(nameof(AllParametres));

                        }
                    )
                    );
            }           
        }
        private int maxID = 1;
        private void ChangeId()
        {
            maxID = 1;
            var k = AllParametres.Select(x => x.Id = maxID++).ToArray();
            
            for (var i = 0; i < AllParametres.Count; i++)
            {
                AllParametres[i].Id = k[i];
            }
            
        }                   
       
        public RelayCommand<Parametres> DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand =
                    new RelayCommand<Parametres>
                    (obj =>
                    {
                       var index =  AllParametres.IndexOf(_currentParametres);
                        if (index <0)
                        {
                            return;
                        }
                        AllParametres.RemoveAt(index);
                        if (index != 0)
                            _currentParametres = AllParametres.ElementAtOrDefault(index - 1);
                        else
                            _currentParametres = AllParametres.FirstOrDefault();
                        ChangeId();
                        OnPropertyChanged(nameof(DeleteCommand));
                        OnPropertyChanged(nameof(AllParametres));
                    }
                    )
                    );
            }
        }

        public Parametres SelectedParametres
        {
            get => _currentParametres;
            set
            {
                _currentParametres = value;
                OnPropertyChanged(nameof(SelectedParametres));
            }
        }


    }
}
