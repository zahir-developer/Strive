namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="JobPayment" />.
    /// </summary>
    [OverrideName("tblJobPayment")]
    public class JobPayment
    {
        /// <summary>
        /// Gets or sets the JobPaymentId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobPaymentId { get; set; }

        /// <summary>
        /// Gets or sets the JobId.
        /// </summary>
        [Column]
        public int? JobId { get; set; }

        /// <summary>
        /// Gets or sets the DrawerId.
        /// </summary>
        [Column]
        public int? DrawerId { get; set; }

        /// <summary>
        /// Gets or sets the PaymentType.
        /// </summary>
        [Column]
        public int? PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        [Column]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the TaxAmount.
        /// </summary>
        [Column]
        public decimal? TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the Cashback.
        /// </summary>
        [Column]
        public decimal? Cashback { get; set; }

        /// <summary>
        /// Gets or sets the CardType.
        /// </summary>
        [Column]
        public int? CardType { get; set; }

        /// <summary>
        /// Gets or sets the CardNumber.
        /// </summary>
        [Column]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the Approval.
        /// </summary>
        [Column]
        public bool? Approval { get; set; }

        /// <summary>
        /// Gets or sets the CheckNumber.
        /// </summary>
        [Column]
        public byte[] CheckNumber { get; set; }

        /// <summary>
        /// Gets or sets the GiftCardId.
        /// </summary>
        [Column]
        public int? GiftCardId { get; set; }

        /// <summary>
        /// Gets or sets the Signature.
        /// </summary>
        [Column]
        public string Signature { get; set; }

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
