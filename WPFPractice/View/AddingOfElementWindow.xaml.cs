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
using WPFPractice.ViewModel;

namespace WPFPractice.View
{
    /// <summary>
    /// Interaction logic for AddingOfElementWindow.xaml
    /// </summary>
    public partial class AddingOfElementWindow : Window
    {
        public AddingOfElementWindow()
        {
            InitializeComponent();
            Loaded += AddingOfElementWindow_Loaded;
            
        }

        private void AddingOfElementWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindows vm)
            {
                vm.Close += () =>
                {
                    this.Close();
                };
                Closing += (s, ex) =>
                  {
                      ex.Cancel = !vm.CanClose();
                  };
            }

        }
    }
}
