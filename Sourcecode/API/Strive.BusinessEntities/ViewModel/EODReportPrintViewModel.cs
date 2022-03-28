using Strive.BusinessEntities.CashRegister.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class EODReportPrintViewModel
    {
        public TimeClockEmployeeHourViewModel EmployeeTimeClock { get; set; }
        public EODSalesReportViewModel Sales { get; set; }
        public CashRegisterDetailViewModel CashRegister { get; set; }
        public CashRegisterDetailViewModel CashInRegister { get; set; }
        public List<DailyStatusReportViewModel> DailyStatusReport { get; set; }
        public DailyStatusViewModel DailyStatusDetailInfoViews { get; set; }

        

    }
}
