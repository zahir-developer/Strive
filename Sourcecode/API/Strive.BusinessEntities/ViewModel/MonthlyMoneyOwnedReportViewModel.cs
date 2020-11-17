using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MonthlyMoneyOwnedReportViewModel
    {
        public DateTime JobDate { get; set; }
        public long ClientId { get; set; }
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public string CustomerName { get; set; }
        public int NumberOfWashes { get; set; }
        public decimal AccountAmount { get; set; }
        public decimal WashesAmount { get; set; }
        public decimal Average { get; set; }
    }
}
