using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblEmployeeSchedule")]
    public class EmployeeSchedule
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int EmployeeScheduleId { get; set; }

        [Column]
        public int? JobId { get; set; }

        [Column]
        public decimal? Percentage { get; set; }

        [Column]
        public decimal? Commision { get; set; }

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