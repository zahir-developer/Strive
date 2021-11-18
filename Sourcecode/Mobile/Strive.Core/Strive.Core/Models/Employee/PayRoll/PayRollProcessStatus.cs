using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Employee.PayRoll
{
    public class PayRoll
    {
        public Result Result { get; set; }
    }

    public class Result
    {
        public List<PayRollRateViewModel> PayRollRateViewModel { get; set; }
    }

    public class PayRollRateViewModel
    {
        public int EmployeeId { get; set; }
        public string PayeeName { get; set; }
        public int LocationId { get; set; }
        public string TotalWashHours { get; set; }
        public string TotalDetailHours { get; set; }
        public string OverTimeHours { get; set; }
        public float WashRate { get; set; }
        public float DetailRate { get; set; }
        public float WashAmount { get; set; }
        public float DetailAmount { get; set; }
        public float OverTimePay { get; set; }
        public float CollisionAmount { get; set; }
        public float DetailCommission { get; set; }
        //public int CategoryId { get; set; }
        //public string CodeValue { get; set; }
        //public string LiabilityType { get; set; }
        //public string LiabilityDetailType { get; set; }
        //public string Amount { get; set; }
        public float Collision { get; set; }
        public float Uniform { get; set; }
        public float Adjustment { get; set; }
        public float PayeeTotal { get; set; }
        public string Notes { get; set; }
        public float CashTip { get; set; }
        public float CardTip { get; set; }
        public float WashTip { get; set; }
        public float DetailTip { get; set; }
        public float Bonous { get; set; }
    }
}
