using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPractice.Cmds;

namespace WPFPractice.ViewModel
{
    class AddingOfElementWindowViewModel : ViewModelBase
    {
        public string Name { get; set; } = String.Empty;
        private RelayCommand _addString;

        public RelayCommand AddString
        {
            get
            {
                return _addString ??
                  (_addString = new RelayCommand(() =>
                  {
                   
                  },
                  ()=>Name.Length>0));
            }

        }


    }
}
