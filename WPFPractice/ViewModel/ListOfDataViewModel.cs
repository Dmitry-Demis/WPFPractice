using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.ViewModel
{
    class ListOfDataViewModel : ViewModelBase
    {
        public ObservableCollection<string> Types
        {
            get;
            set;
        } = new ObservableCollection<string>();
        public ListOfDataViewModel()
        {
           
            int v = 1;
            Types.Add($"Значение {v++}");
            Types.Add($"Значение {v++}");
            Types.Add($"Значение {v++}");
            OnPropertyChanged(nameof(Types));
        }
    }
}
