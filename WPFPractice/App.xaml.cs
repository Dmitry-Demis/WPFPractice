using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFPractice.Model;
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
           
            IDialogService dialogService = new DialogService(MainWindow);
            dialogService.Register<AddingOfElementWindowViewModel, AddingOfElementWindow>();
            dialogService.Register<ChangeParameterViewModel, ChangeParameterWindow>();
            var viewModel = new MainWindowViewModel(dialogService);
            var view = new MainWindow { DataContext = viewModel };

            view.ShowDialog();




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
