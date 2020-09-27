using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementController.Services;
using System.Windows.Input;
using Microsoft.Win32;
using System.Diagnostics;

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
        ExcelService xLSX;

        public LoginViewModel()
        {
            userService = new UserService();
        }
        private async void Register()
        {
            if (!userService.IsUserExists(Username).Result)
            {
                Trace.WriteLine("User does NOT exists, try to register");
                var userService = new UserService();
                await userService.RegisterUser(Username, Password);
                Trace.WriteLine("try to login");
                Thread.Sleep(8000);
            }
            else
            {
                Trace.WriteLine("User exists");
            }
            Id = Login().Result;
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
            xLSX = new ExcelService(Url);
        }

        public async Task LoginButton()
        {
            await Login();
            Thread.Sleep(5000);
            await xLSX.AddData(Url);
        }

        public async Task RegisterButton()
        {
            Register();
            Thread.Sleep(5000);
            await xLSX.AddData(Url);
        }

        private async Task<string> Login()
        {
            var userService = new UserService();
            bool Result = await userService.Login(Username, Password);
            if (Result)
            {
                Trace.WriteLine("logged");
                Trace.WriteLine(userService.user.Id);
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
