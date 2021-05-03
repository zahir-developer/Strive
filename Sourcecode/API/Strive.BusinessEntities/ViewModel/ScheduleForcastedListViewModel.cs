using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleForcastedListViewModel
    {
        public ScheduleTotalWashHoursViewModel ScheduleHoursViewModel { get; set; }
        public ScheduleTotalEmployeeViewModel ScheduleEmployeeViewModel { get; set; }
        public ForcastedCarEmployeehoursViewModel ForcastedCarEmployeehoursViewModel { get; set; }
    }
}
