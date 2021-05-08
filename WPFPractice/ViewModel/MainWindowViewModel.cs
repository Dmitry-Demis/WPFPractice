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

namespace WPFPractice.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Parametres _currentParametres;
        public ObservableCollection<string> Types 
        {
            get;
        }
        public MainWindowViewModel()
        {
            Types = new ObservableCollection<string>();
            Types.Add("Простая строка");
            Types.Add("Строка с историей");
            Types.Add("Значение из списка");
            Types.Add("Набор значений из списка");
            OnPropertyChanged(nameof(Types));
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
                            using (var sw = new StreamWriter(saveDlg.FileName, false, Encoding.UTF8))
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
                        OnPropertyChanged(nameof(SaveCommand));
                    }
                    )
                    );
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
