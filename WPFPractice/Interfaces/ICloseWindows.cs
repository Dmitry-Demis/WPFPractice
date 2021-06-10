using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.Model
{
    public interface ICloseWindows
    {
        Action Close { get; set; }
        bool CanClose();
    }
}
