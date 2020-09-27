using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TimeManagementController.Models;
using TimeManagementController.ViewModels;

namespace TimeManagementController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string text;
        

        LoginViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }

   

        private async void OpenFileDIalog(object sender, RoutedEventArgs e)
        {
            viewModel.FileDialogClick();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("dffdf");
            viewModel.LoginButton();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            viewModel.RegisterButton();
        }
    }
}
