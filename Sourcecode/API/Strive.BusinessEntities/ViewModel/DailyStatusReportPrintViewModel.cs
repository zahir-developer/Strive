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
        public DailyStatusViewModel DailyStatusDetailInfoViews { get; set; }

        public List<DailyStatusReportViewModel> DailyStatusReport { get; set; }

    }
}
