using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.Model
{
    public interface IFileService
    {
        List<Parametres> Open(string filename);
        void Save(string filename, List<Parametres> parametresList);
    }
}
