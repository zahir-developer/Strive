using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PayRollRateViewModel
    {
        public int EmployeeId { get; set; }
        public string PayeeName { get; set; }
        public int LocationId { get; set; }
        public string TotalWashHours { get; set; }
        public string TotalDetailHours { get; set; }
        public string OverTimeHours { get; set; }
        public decimal WashRate { get; set; }
        public decimal DetailRate { get; set; }
        public decimal? WashAmount { get; set; }
        public decimal? DetailAmount { get; set; }
        public decimal? OverTimePay { get; set; }
        public decimal CollisionAmount { get; set; }
        public decimal DetailCommission { get; set; }
        public int? CategoryId { get; set; }
        public string CodeValue { get; set; }
        public int? LiabilityType { get; set; }
        public int? LiabilityDetailType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Collision { get; set; }
        public decimal? Uniform { get; set; }
        public decimal? Adjustment { get; set; }
        public decimal PayeeTotal { get; set; }
        public string Notes { get; set; }
        public decimal? CashTip { get; set; }
        public decimal? CardTip { get; set; }
        public decimal? WashTip { get; set; }
        public decimal? DetailTip { get; set; }
        public decimal? Bonus { get; set; }
        public bool IsClockedOut { get; set; }
    }
}
