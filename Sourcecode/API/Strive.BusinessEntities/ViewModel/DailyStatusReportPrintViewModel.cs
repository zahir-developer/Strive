using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DailyStatusReportPrintViewModel
    {
        public TimeClockEmployeeHourViewModel EmployeeTimeClock { get; set; }        
        public List<DailyStatusDetailInfoViewModel> DailyStatusDetailInfoViews { get; set; }

    }
}
