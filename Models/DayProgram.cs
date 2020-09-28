using System.Collections.Generic;

namespace TimeManagementController.Models
{
    class DayProgram:List<Activity>
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
