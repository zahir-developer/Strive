using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleViewModel
    {
        public List<ScheduleDetailViewModel> ScheduleDetailViewModel { get; set; }
        public ScheduleTotalWashHoursViewModel ScheduleHoursViewModel { get; set; }
        public ScheduleTotalEmployeeViewModel ScheduleEmployeeViewModel { get; set; }

    }
}
