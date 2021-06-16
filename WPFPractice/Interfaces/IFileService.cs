using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPractice.Model;

namespace WPFPractice.Interfaces
{
    public interface IFileService
    {
        List<Parameter> Open(string fileName);
        void Save(string fileName, List<Parameter> parameters);
    }
}
