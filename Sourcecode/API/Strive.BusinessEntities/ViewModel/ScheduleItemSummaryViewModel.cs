using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ScheduleItemSummaryViewModel
    {
        public decimal? Tax { get; set; }
        public decimal? Cashback { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Total { get; set; }
        public decimal? GrandTotal { get; set; }
    }
}
