using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementController.Services;

namespace TimeManagementController.ViewModels
{
    class LoginViewModel
    {
        private bool Result;

        public async Task Register(string Username1, string Password1)
        {
            var userService = new UserService();
            Result = await userService.RegisterUser(Username1, Password1);
            if (Result)
                Console.WriteLine("OK");
            else
                Console.WriteLine("exists");
        }


        public async Task<string> LogOrReg(string Username1, string Password1)
        {
            var userService = new UserService();
            if (userService.IsUserExists(Username1).Result)
            {
                Console.WriteLine("User exists, try to login");
            }
            else
            {
                Console.WriteLine("User does NOT exists, try to register");
                Register(Username1, Password1);
                Console.WriteLine("try to login");
                Thread.Sleep(8000);
            }
            return Login(Username1, Password1).Result;
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
