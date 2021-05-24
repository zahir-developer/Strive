using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblPayrollProcess")]
    public class PayrollProcess
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int PayrollProcessId { get; set; }

        [Column]
        public DateTime? FromDate { get; set; }
        [Column]
        public DateTime? ToDate { get; set; }
        [Column]
        public int? LocationId { get; set; }


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