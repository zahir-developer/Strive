using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleViewModel
    {
        public ScheduleDetailViewModel ScheduleDetailViewModel { get; set; }
        public ScheduleHoursViewModel ScheduleHoursViewModel { get; set; }
        public ScheduleEmployeeViewModel ScheduleEmployeeViewModel { get; set; }

    }
}
