using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.TimeClock
{
    public class TimeClockDto
    {
        public int LocationId { get; set; }
        public int EmployeeId { get; set; }
        public int? RoleId { get; set; }
        public DateTime Date { get; set; }
    }
}
