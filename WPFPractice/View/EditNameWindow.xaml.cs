using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WPFPractice.ViewModel;

namespace WPFPractice.View
{
    /// <summary>
    /// Interaction logic for AddingOfElementWindow.xaml
    /// </summary>
    public partial class EditNameWindow : Window, IDialog
    {
        public EditNameWindow()
        {
            InitializeComponent();            
        }
    }
}
