using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.TimeClock
{
    public class TimeClockWeekDetailDto
    {
        public int EmployeeId { get; set; }

        public int LocationId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string EmployeeName { get; set; }
    }
}
