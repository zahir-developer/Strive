namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="NotificationTemplate" />.
    /// </summary>
    [OverrideName("tblNotificationTemplate")]
    public class NotificationTemplate
    {
        /// <summary>
        /// Gets or sets the NotificationId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int NotificationId { get; set; }

        /// <summary>
        /// Gets or sets the NotificationName.
        /// </summary>
        [Column]
        public string NotificationName { get; set; }

        /// <summary>
        /// Gets or sets the NotificationType.
        /// </summary>
        [Column]
        public int? NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the NotificationMessage.
        /// </summary>
        [Column]
        public string NotificationMessage { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the IsInternal.
        /// </summary>
        [Column]
        public bool? IsInternal { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        [Column]
        public bool? IsActive { get; set; }

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
