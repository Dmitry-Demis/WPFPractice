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

namespace WPFPractice.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        List<Parameter> Parameters { get; set; } = new List<Parameter>();
        private DataType _currentElement;
        public DataType CurrentElement
        {
            get => _currentElement;
            set
            {
                if (_currentElement!=value)
                {
                    _currentElement = value;
                    OnPropertyChanged(nameof(CurrentElement));
                }
            }
        }
        private DataType _defaultElement;

        public DataType DefaultElement
        {
            get { return _defaultElement; }
            set { _defaultElement = value; }
        }
        private RelayCommand _addItem;

        public RelayCommand AddItem
        {
            get
            {
                return _addItem ??
                  (_addItem = new RelayCommand( ()=>
                  {
                      Parameter parameter = new Parameter();
                      AddingOfElementWindow addingOfElementWindow = new AddingOfElementWindow();
                      addingOfElementWindow.Show();
                  }));
            }
           
        }

    }
}
