namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="CashRegisterBills" />.
    /// </summary>
    [OverrideName("tblCashRegisterBills")]
    public class CashRegisterBills
    {
        /// <summary>
        /// Gets or sets the CashRegBillId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int CashRegBillId { get; set; }

        /// <summary>
        /// Gets or sets the s1.
        /// </summary>
        [Column]
        [OverrideName("1s")]
        public int? s1 { get; set; }

        /// <summary>
        /// Gets or sets the s5.
        /// </summary>
        [Column]
        [OverrideName("5s")]
        public int? s5 { get; set; }

        /// <summary>
        /// Gets or sets the s10.
        /// </summary>
        [Column]
        [OverrideName("10s")]
        public int? s10 { get; set; }

        /// <summary>
        /// Gets or sets the s20.
        /// </summary>
        [Column]
        [OverrideName("20s")]
        public int? s20 { get; set; }

        /// <summary>
        /// Gets or sets the s50.
        /// </summary>
        [Column]
        [OverrideName("50s")]
        public int? s50 { get; set; }

        /// <summary>
        /// Gets or sets the s100.
        /// </summary>
        [Column]
        [OverrideName("100s")]
        public int? s100 { get; set; }

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
