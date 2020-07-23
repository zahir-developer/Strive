namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="TimeClock" />.
    /// </summary>
    [OverrideName("tblTimeClock")]
    public class TimeClock
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
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the RoleId.
        /// </summary>
        [Column]
        public int? RoleId { get; set; }

        /// <summary>
        /// Gets or sets the EventDate.
        /// </summary>
        [Column]
        public DateTimeOffset? EventDate { get; set; }

        /// <summary>
        /// Gets or sets the InTime.
        /// </summary>
        [Column]
        public DateTimeOffset? InTime { get; set; }

        /// <summary>
        /// Gets or sets the OutTime.
        /// </summary>
        [Column]
        public DateTimeOffset? OutTime { get; set; }

        /// <summary>
        /// Gets or sets the EventType.
        /// </summary>
        [Column]
        public int? EventType { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedFrom.
        /// </summary>
        [Column]
        public string UpdatedFrom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Status.
        /// </summary>
        [Column]
        public bool Status { get; set; }

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
