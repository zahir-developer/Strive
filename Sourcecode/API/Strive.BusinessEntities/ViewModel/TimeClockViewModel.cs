using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TimeClockViewModel
    {
        public int TimeClockId { get; set; }
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string Day { get; set; }
        public DateTime? EventDate { get; set; }
        public DateTimeOffset? InTime { get; set; }
        public DateTimeOffset? OutTime { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
        public DateTimeOffset? TotalHours { get; set; }
        public int? EventType { get; set; }
        public bool Status { get; set; }
    }
}
