using System.Diagnostics;
using System.Windows;
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
            Trace.WriteLine("filedialog");
            await viewModel.FileDialogClick();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("login button");
            await viewModel.LoginButton();
        }

        private async void Register(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("register button");
            await viewModel.RegisterButton();
            MessageBox.Show("Done");
        }
    }
}
