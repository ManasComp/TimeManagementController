using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementController.Models
{
    class Activity
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Name { get; set; }
        public int Day { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
