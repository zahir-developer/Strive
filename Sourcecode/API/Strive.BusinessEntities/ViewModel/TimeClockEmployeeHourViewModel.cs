using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TimeClockEmployeeHourViewModel
    {
        public List<TimeClockEmployeeViewModel> TimeClockEmployeeDetails { get; set; }
        public List<TimeClockViewModel> TimeClockDetails { get; set; }
    }
}
