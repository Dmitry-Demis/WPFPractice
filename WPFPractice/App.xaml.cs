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
            //var viewModel = new MainWindowViewModel();
            var view = new MainWindow ();
            view.Show();
            //viewAdding.Show();

            //Rem: что-то подобное д.получиться
            //IDialogService dialogService = new DialogService(MainWindow);
            //dialogService.Register<ListEditViewModel, ListEditWindow>();
            //dialogService.Register<LineEditViewModel, LineEditWindow>();
            //var viewModel = new MainWindowViewModel(dialogService);
            //var view = new MainWindow { DataContext = viewModel };
            //view.ShowDialog();
        }
    }
}
