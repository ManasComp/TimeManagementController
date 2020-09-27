using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementController.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimeManagementController.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private bool Result;

        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetValue(ref _id, value);
            }
        }
        public string Username { get; set; }
        public string Password { get; set; }


        public async Task Register(string Username1, string Password1)
        {
            var userService = new UserService();
            Result = await userService.RegisterUser(Username1, Password1);
            if (Result)
                Console.WriteLine("OK");
            else
                Console.WriteLine("exists");
        }


        public async Task<string> LogOrReg()
        {
            var userService = new UserService();
            if (userService.IsUserExists(Username).Result)
            {
                Console.WriteLine("User exists, try to login");
            }
            else
            {
                Console.WriteLine("User does NOT exists, try to register");
                Register(Username, Password);
                Console.WriteLine("try to login");
                Thread.Sleep(8000);
            }
            Id= Login(Username, Password).Result;
            return Id;
        }

        public async Task<string> Login(string Username1, string Password1)
        {
            var userService = new UserService();
            Result = await userService.Login(Username1, Password1);
            if (Result)
            {
                Console.WriteLine("logged");
                Console.WriteLine(userService.user.Id);
                return userService.user.Id;

            }
            else
            {
                Console.WriteLine("error");
                return "";
            }
        }
    }
}
