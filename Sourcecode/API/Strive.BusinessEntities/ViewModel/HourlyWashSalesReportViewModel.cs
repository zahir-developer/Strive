using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class HourlyWashSalesReportViewModel
    {
        public List<WashHoursViewModel> WashHoursViewModel { get; set; }
        public List<SalesSummaryViewModel> SalesSummaryViewModel { get; set; }
        public List<LocationWashServiceViewModel> LocationWashServiceViewModel { get; set; }

    }
}
