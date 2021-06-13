using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Homework_16_1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IDialogService dialogService = new DialogService(MainWindow);
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(dialogService);
            MainWindow window = new MainWindow();
            window.DataContext = mainWindowViewModel;
            window.ShowDialog();
        }
    }
}
