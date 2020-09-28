using Firebase.Database;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimeManagementController.Models;

namespace TimeManagementController.Services
{
    class UserService
    {
        FirebaseService firebaseService;
        public UserService()
        {
            firebaseService = new FirebaseService();
        }
    
        public User user;
        public async Task<bool> RegisterUser(string uname, string passwd)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = uname,
                Password = passwd
            };
            //string _url = "https://timemanegment-74160.firebaseio.com/";
            //FirebaseClient firebaseClient = new FirebaseClient(_url);
            //await firebaseClient.Child("Users").PostAsync(user);
            await firebaseService.PostAsync("Users", user);
            Trace.WriteLine("ready");
            return true;
        }

        public async Task<bool> IsUserExists(string uname)
        {
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient firebaseClient = new FirebaseClient(_url);
            User user = firebaseClient.Child("Users").OnceAsync<User>().Result.Select(e => e.Object as User).ToList().FirstOrDefault(u => u.Username == uname);


            //User user = firebaseService.OnceAsync<User>("Users").Result.FirstOrDefault(u => u.Username == uname);

            return (user != null);
        }

        public async Task<bool> Login(string uname, string passwd)
        {
            string _url = "https://timemanegment-74160.firebaseio.com/";
            FirebaseClient _firebaseClient = new FirebaseClient(_url);
            var mrdka = _firebaseClient.Child("Users").OnceAsync<User>();
            user = mrdka.Result.Select(e => e.Object as User).Where(u => u.Username == uname)
                .FirstOrDefault(u => u.Password == passwd);
            //user = firebaseService.OnceAsync<User>("Users").Result.Where(u => u.Username == uname)
            //    .FirstOrDefault(u => u.Password == passwd);
            return (user != null);
        }
    }
}
