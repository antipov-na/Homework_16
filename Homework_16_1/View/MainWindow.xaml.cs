using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework_16_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DialogService dialogService;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            dialogService = new DialogService(this);
            dialogService.Register<GenerateClientsViewModel, GenerateClientsWindow>();
        }
    }
}
