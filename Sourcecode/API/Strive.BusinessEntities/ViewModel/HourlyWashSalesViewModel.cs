using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class HourlyWashSalesViewModel
    {
        public List<SalesSummaryViewModel> SalesSummaryViewModel { get; set; }
        public List<LocationWashServiceViewModel> LocationWashServiceViewModel { get; set; }

    }
}
