namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="CashRegisterRolls" />.
    /// </summary>
    [OverrideName("tblCashRegisterRolls")]
    public class CashRegisterRolls
    {
        /// <summary>
        /// Gets or sets the CashRegRollId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int CashRegRollId { get; set; }

        /// <summary>
        /// Gets or sets the Pennies.
        /// </summary>
        [Column]
        public int? Pennies { get; set; }

        /// <summary>
        /// Gets or sets the Nickels.
        /// </summary>
        [Column]
        public int? Nickels { get; set; }

        /// <summary>
        /// Gets or sets the Dimes.
        /// </summary>
        [Column]
        public int? Dimes { get; set; }

        /// <summary>
        /// Gets or sets the Quarters.
        /// </summary>
        [Column]
        public int? Quarters { get; set; }

        /// <summary>
        /// Gets or sets the HalfDollars.
        /// </summary>
        [Column]
        public int? HalfDollars { get; set; }

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
