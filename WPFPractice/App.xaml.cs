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
            dialogService.Register<EditNameViewModel, EditNameWindow>();
            dialogService.Register<ChangeParameterViewModel, ChangeParameterWindow>();
            var viewModel = new MainWindowViewModel(dialogService);
            var view = new MainWindow { DataContext = viewModel };            
            view.ShowDialog();
        }
    }
}
