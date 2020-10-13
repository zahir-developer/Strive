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
        public int TotalWashHours { get; set; }
        public int TotalDetailHours { get; set; }
        public int OverTimeHours { get; set; }
        public decimal WashRate { get; set; }
        public string DetailRate { get; set; }
        public decimal WashAmount { get; set; }
        public decimal DetailAmount { get; set; }
        public decimal OverTimePay { get; set; }
        public decimal CollisionAmount { get; set; }
        public decimal DetailCommission { get; set; }
        public decimal PayeeTotal { get; set; }

    }
}
