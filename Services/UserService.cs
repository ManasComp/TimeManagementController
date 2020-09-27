using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using TimeManagementController.Models;

namespace TimeManagementController.Services
{
    class UserService
    {
        //private FirebaseService _firebaseService;
        public User user;
        public async Task<bool> RegisterUser(string uname, string passwd)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = uname,
                Password = passwd
            };
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient firebaseClient = new FirebaseClient(_url);
            //firebaseClient.Child("Users1").DeleteAsync();
            firebaseClient.Child("Users").PostAsync(user);
            Trace.WriteLine("ready");
            return true;
        }

        public async Task<bool> IsUserExists(string uname)
        {
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient firebaseClient = new FirebaseClient(_url);
            User user = firebaseClient.Child("Users").OnceAsync<User>().Result.Select(e => e.Object as User)
                .FirstOrDefault(u => u.Username == uname);

            return (user != null);
        }

        public async Task<bool> Login(string uname, string passwd)
        {
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient _firebaseClient = new FirebaseClient(_url);
            var mrdka = _firebaseClient.Child("Users").OnceAsync<User>();
            user = mrdka.Result.Select(e => e.Object as User).ToList()
                .Where(u => u.Username == uname)
                .FirstOrDefault(u => u.Password == passwd);
            return (user != null);
        }
    }
}
