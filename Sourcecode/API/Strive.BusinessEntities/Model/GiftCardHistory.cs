namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="GiftCardHistory" />.
    /// </summary>
    [OverrideName("tblGiftCardHistory")]
    public class GiftCardHistory
    {
        /// <summary>
        /// Gets or sets the GiftCardHistoryId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int GiftCardHistoryId { get; set; }

        /// <summary>
        /// Gets or sets the GiftCardId.
        /// </summary>
        [Column]
        public int? GiftCardId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the TransactionType.
        /// </summary>
        [Column]
        public int? TransactionType { get; set; }

        /// <summary>
        /// Gets or sets the TransactionAmount.
        /// </summary>
        [Column]
        public decimal? TransactionAmount { get; set; }

        /// <summary>
        /// Gets or sets the TransactionUserId.
        /// </summary>
        [Column]
        public int? TransactionUserId { get; set; }

        /// <summary>
        /// Gets or sets the TransactionDate.
        /// </summary>
        [Column]
        public DateTimeOffset? TransactionDate { get; set; }

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
