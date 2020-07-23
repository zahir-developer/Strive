namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="GiftCard" />.
    /// </summary>
    [OverrideName("tblGiftCard")]
    public class GiftCard
    {
        /// <summary>
        /// Gets or sets the GiftCardId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int GiftCardId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the GiftCardCode.
        /// </summary>
        [Column]
        public string GiftCardCode { get; set; }

        /// <summary>
        /// Gets or sets the GiftCardName.
        /// </summary>
        [Column]
        public string GiftCardName { get; set; }

        /// <summary>
        /// Gets or sets the ExpiryDate.
        /// </summary>
        [Column]
        public DateTimeOffset? ExpiryDate { get; set; }

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
