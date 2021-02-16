using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblPayrollEmploylee")]
    public class PayrollEmployee
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int PayrollEmployeeId { get; set; }

        [Column]
        public int? EmployeeId { get; set; }
        

        [Column]
        public int? PayrollProcessId { get; set; }
        
        [Column]
        public decimal? Adjustment { get; set; }
        

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