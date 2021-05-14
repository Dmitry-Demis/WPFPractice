using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFPractice.View;
using WPFPractice.ViewModel;

namespace WPFPractice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var viewModel = new MainWindowViewModel();
            var view = new MainWindow { DataContext = viewModel };
            var viewModelAddingElement = new AddingOfElementWindowViewModel();
            var viewAdding = new AddingOfElementWindow { DataContext = viewModelAddingElement };
            view.Show();
            //viewAdding.Show();
        }
    }
}
