using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DetailScheduleViewModel
    {
        public List<Schedule> Schedule { get; set; }
        
        public List<BaySchedule> BaySchedule { get; set; }
        
    }
    //Below seperation will be moved as new class file after completion
    public class Schedule
    {
        public int JobId { get; set; }
        public string TimeIn { get; set; }
    }
    public class BaySchedule
    {
        public int? JobId { get; set; }
        public int? BayBayId { get; set; }
        public int? JobBayId { get; set; }
        public DateTime? JobDate { get; set; }
        public string IsSchedule { get; set; }
    }

}
