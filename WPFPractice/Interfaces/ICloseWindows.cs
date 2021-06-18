using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.Model
{
    public interface ICloseWindows
    {
        event Action Closed;
        void Close();
    }
}
