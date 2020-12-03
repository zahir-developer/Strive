using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class HourlyWashViewModel
    {
        public SalesSummaryViewModel SalesSummaryViewModel { get; set; }
        public List<WashHoursViewModel> WashHoursViewModel { get; set; }

    }
}
