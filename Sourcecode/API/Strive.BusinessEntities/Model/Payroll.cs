using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblPayroll")]
    public class Payroll
    {

        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int PayrollId { get; set; }

        [Column]
        public int? EmployeeId { get; set; }

        [Column]
        public string PayeeName { get; set; }

        [Column]
        public int? WashHours { get; set; }

        [Column]
        public int? DetailHours { get; set; }

        [Column]
        public decimal? Rate { get; set; }

        [Column]
        public decimal? RegularPay { get; set; }

        [Column]
        public int? OverTimeHours { get; set; }

        [Column]
        public decimal? OverTimePay { get; set; }

        [Column]
        public decimal? Collison { get; set; }

        [Column]
        public decimal? Uniform { get; set; }

        [Column]
        public decimal? Adjustment { get; set; }

        [Column]
        public decimal? DetailCommision { get; set; }

        [Column]
        public decimal? Tips { get; set; }

        [Column]
        public decimal? PayeeTotal { get; set; }

        [Column]
        public string Comments { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }

        [Column]
        public int? CreatedBy { get; set; }

        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }

        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }

    }
}