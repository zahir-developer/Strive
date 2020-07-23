namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="Schedule" />.
    /// </summary>
    [OverrideName("tblSchedule")]
    public class Schedule
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        [Column]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the RoleId.
        /// </summary>
        [Column]
        public int? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the ScheduledDate.
        /// </summary>
        [Column]
        public DateTimeOffset? ScheduledDate { get; set; }

        /// <summary>
        /// Gets or sets the StartTime.
        /// </summary>
        [Column]
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the EndTime.
        /// </summary>
        [Column]
        public DateTimeOffset? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the ScheduleType.
        /// </summary>
        [Column]
        public int? ScheduleType { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        [Column]
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        [Column]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy.
        /// </summary>
        [Column]
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy.
        /// </summary>
        [Column]
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
