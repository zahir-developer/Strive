using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblBaySchedule")]
    public class BaySchedule
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int BayScheduleId { get; set; }

        [Column]
        public int BayId { get; set; }

        [Column]
        public int? JobId { get; set; }

        [Column]
        public DateTime? ScheduleDate { get; set; }

        [Column]
        public TimeSpan? ScheduleInTime { get; set; }

        [Column]
        public TimeSpan? ScheduleOutTime { get; set; }

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