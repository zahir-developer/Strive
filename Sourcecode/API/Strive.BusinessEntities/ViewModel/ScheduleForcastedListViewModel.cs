using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleForcastedListViewModel
    {
        public List<ScheduleTotalWashHoursViewModel> ScheduleHoursViewModel { get; set; }
        public List<ScheduleTotalEmployeeViewModel> ScheduleEmployeeViewModel { get; set; }
        public List<ForcastedCarEmployeehoursViewModel> ForcastedCarEmployeehoursViewModel { get; set; }
    }
}
