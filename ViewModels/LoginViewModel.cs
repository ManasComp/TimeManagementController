using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;
using TimeManagementController.Services;

namespace TimeManagementController.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetValue(ref _id, value);
        }

        private string _url;
        public string Url
        {
            get => _url;
            set => SetValue(ref _url, value);
        }

        public string Username { get; set; }
        public string Password { get; set; }
        UserService userService;
        private readonly PageService _pageService;
        ExcelService xLSX;

        public LoginViewModel()
        {
            userService = new UserService();
            _pageService = new PageService();
        }
        private async Task Register()
        {
            if (!userService.IsUserExists(Username).Result)
            {
                Trace.WriteLine("User does NOT exists, try to register");
                var userService = new UserService();
                await userService.RegisterUser(Username, Password);
                Trace.WriteLine("try to login");
                //Thread.Sleep(8000);
            }
            else
            {
                Trace.WriteLine("User exists");
            }
            await Login();
        }

        public async Task FileDialogClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Url = openFileDialog.FileName;
            }
        }

        public async Task LoginButton()
        {
            if (Login().Result != "")
            {
                xLSX = new ExcelService(Id, Url);
                xLSX.AddData();
                _pageService.Massage("done");
            }
            else
            {
                _pageService.Massage("Invalid credentials");
            }
        }

        public async Task RegisterButton()
        {
            await Register();
            xLSX = new ExcelService(Id, Url);
            await xLSX.AddData();
        }

        private async Task<string> Login()
        {
            var userService = new UserService();
            bool Result = await userService.Login(Username, Password);
            if (Result)
            {
                Trace.WriteLine("logged");
                Trace.WriteLine(userService.user.Id);
                Id = userService.user.Id;
                return userService.user.Id;
            }
            else
            {
                Trace.WriteLine("error");
                return "";
            }
        }
    }
}
