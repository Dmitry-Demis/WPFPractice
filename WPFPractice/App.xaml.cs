using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFPractice.Interfaces;
using WPFPractice.Model;
using WPFPractice.Services;
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
            IFileService fileService = new JsonFileService();
            dialogService.Register<EditNameViewModel, EditNameWindow>();
            dialogService.Register<EditValuesListViewModel, ChangeParameterWindow>();
            var viewModel = new MainWindowViewModel(dialogService, fileService);
            var view = new MainWindow { DataContext = viewModel };            
            view.ShowDialog();
        }
    }
}
