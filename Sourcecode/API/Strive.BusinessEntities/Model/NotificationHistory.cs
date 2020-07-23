namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="NotificationHistory" />.
    /// </summary>
    [OverrideName("tblNotificationHistory")]
    public class NotificationHistory
    {
        /// <summary>
        /// Gets or sets the NotificationHistoryId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int NotificationHistoryId { get; set; }

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
