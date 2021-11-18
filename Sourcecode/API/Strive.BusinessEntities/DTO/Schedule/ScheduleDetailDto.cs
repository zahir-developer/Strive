using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Schedule
{
    public class ScheduleDetailDto
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int locationId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
