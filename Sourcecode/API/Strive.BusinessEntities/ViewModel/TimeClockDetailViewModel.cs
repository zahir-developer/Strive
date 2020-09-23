using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TimeClockDetailViewModel
    {
        public List<TimeClockEmployeeDetailViewModel> TimeClockEmployeeDetailViewModel { get; set; }
        public List<NotClockedInEmployeeViewModel> EmployeeViewModel { get; set; }
    }
}
