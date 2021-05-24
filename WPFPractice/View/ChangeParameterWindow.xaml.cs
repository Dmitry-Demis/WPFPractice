using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFPractice.Model;

namespace WPFPractice.View
{
    /// <summary>
    /// Interaction logic for ListOfData.xaml
    /// </summary>
    public partial class ChangeParameterWindow : Window, IDialog //Rem: [solved] название ListOfData не сильно говорящее 
    {
        public ChangeParameterWindow()
        {
            InitializeComponent();
        }
    }
}
