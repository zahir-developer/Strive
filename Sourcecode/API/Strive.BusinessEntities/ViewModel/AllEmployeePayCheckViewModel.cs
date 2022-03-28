using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class AllEmployeePayCheckViewModel
    {
        public int EmployeeId { get; set; }
        public string PayeeName { get; set; }
        public int LocationId { get; set; }
        public decimal? WashRate { get; set; }
        public decimal? TotalWashHours { get; set; }
        public decimal? TotalDetailHours { get; set; }
        public decimal? WashAmount { get; set; }
        public decimal? OverTimePay { get; set; }
        public int WashCount { get; set; }
        public decimal? DetailCommission { get; set; }
        public decimal? BonusAmount { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? NetPay { get; set; }
    }
}
