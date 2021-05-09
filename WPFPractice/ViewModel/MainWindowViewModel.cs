using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPractice.Model;
using WPFPractice.Cmds;
using WPFPractice.View;
using WPFPractice;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace WPFPractice.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Parametres _currentParametres;
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
                            parametres.NameOfParametre = $"Параметр {parametres.Id}";
                            SelectedParametres = parametres;
                            AllParametres.Insert(AllParametres.Count, parametres);                           
                        }
                    )
                    );
            }           
        }
        private RelayCommand<Parametres> _closeAWindow;
        public RelayCommand<Parametres> CloseAWindow
        {
            get
            {
                return _closeAWindow ?? (_closeAWindow =
                    new RelayCommand<Parametres>
                    (obj =>
                    {
                        string msg = "Сохранить и выйти?";
                        MessageBoxResult result = MessageBox.Show(msg, "Закрытие приложения", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            SaveWithoutDialog("result.csv");
                        }
                        else
                        {
                            
                        }
                        OnPropertyChanged(nameof(CloseAWindow));
                    }
                    )
                    );
            }
        }

        private RelayCommand<Parametres> _saveCommand;
        public RelayCommand<Parametres> SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand =
                    new RelayCommand<Parametres>
                    (obj =>
                    {
                        var saveDlg = new SaveFileDialog ();
                        saveDlg.Filter = "Comma-Separated Values|*.csv";
                        saveDlg.Title = "Сохраняем csv файл";
                        saveDlg.FileName = "result.csv";
                        
                        if (true == saveDlg.ShowDialog())
                        {
                            SaveWithoutDialog(saveDlg.FileName);
                        }
                        OnPropertyChanged(nameof(SaveCommand));
                    }
                    )
                    );
            }
        }
        private void SaveWithoutDialog(string fileName)
        {
            using (var sw = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                bool sep = false;
                sw.WriteLine("Id; Название");
                foreach (var item in AllParametres)
                {
                    if (sep)
                    {
                        sw.WriteLine(";");
                    }
                    sep = true;
                    sw.Write($"{item.Id}; {item.NameOfParametre}");
                }
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
