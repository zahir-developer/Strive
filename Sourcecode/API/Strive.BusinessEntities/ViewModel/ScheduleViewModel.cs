using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleViewModel
    {
        public int ScheduleId { get; set; }

     
        public int? EmployeeId { get; set; }


        public int? LocationId { get; set; }

        public bool? IsEmployeeAbscent { get; set; }

        public int? RoleId { get; set; }


        public DateTime? ScheduledDate { get; set; }


        public DateTimeOffset? StartTime { get; set; }

  
        public DateTimeOffset? EndTime { get; set; }


        public int? ScheduleType { get; set; }

 
        public string Comments { get; set; }

        public string EployeeRole { get; set; }
        public string EmployeeName { get; set; }
        public string LocationName { get; set; }

    }
}
