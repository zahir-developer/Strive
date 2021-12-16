using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MonthlyMoneyOwedReportViewModel
    {
        public int ClientId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int NumberOfWashes { get; set; }
        public decimal? MembershipAmount { get; set; }
        public decimal TotalJobAmount { get; set; }
        public decimal? MoneyOwed { get; set; }
        public decimal Average { get; set; }
        public int? TotalWashCount { get; set; }
    }
}
