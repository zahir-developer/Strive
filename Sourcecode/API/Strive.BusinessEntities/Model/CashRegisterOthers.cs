namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="CashRegisterOthers" />.
    /// </summary>
    [OverrideName("tblCashRegisterOthers")]
    public class CashRegisterOthers
    {
        /// <summary>
        /// Gets or sets the CashRegOtherId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int CashRegOtherId { get; set; }

        /// <summary>
        /// Gets or sets the CreditCard1.
        /// </summary>
        [Column]
        public decimal? CreditCard1 { get; set; }

        /// <summary>
        /// Gets or sets the CreditCard2.
        /// </summary>
        [Column]
        public decimal? CreditCard2 { get; set; }

        /// <summary>
        /// Gets or sets the CreditCard3.
        /// </summary>
        [Column]
        public decimal? CreditCard3 { get; set; }

        /// <summary>
        /// Gets or sets the Checks.
        /// </summary>
        [Column]
        public decimal? Checks { get; set; }

        /// <summary>
        /// Gets or sets the Payouts.
        /// </summary>
        [Column]
        public decimal? Payouts { get; set; }

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
