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
using TimeManagementController.ViewModels;

namespace TimeManagementController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new LoginViewModel();
            this.DataContext = viewModel;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Name");
            //string name = Console.ReadLine();
            Console.WriteLine("pass");
            //string pass = Console.ReadLine();

            string id = viewModel.LogOrReg().Result;

            Console.WriteLine(id);
            Console.WriteLine("Start");
            Thread.Sleep(5000);
            TimeManagementController.Services.ExcelService xLSX = new TimeManagementController.Services.ExcelService(id);
            Thread.Sleep(5000);
            await xLSX.AddData();
            Console.WriteLine("end");
            //Console.ReadKey();
        }
    }
}
