namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="CashRegister" />.
    /// </summary>
    [OverrideName("tblCashRegister")]
    public class CashRegister
    {
        /// <summary>
        /// Gets or sets the CashRegisterId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int CashRegisterId { get; set; }

        /// <summary>
        /// Gets or sets the CashRegisterType.
        /// </summary>
        [Column]
        public int? CashRegisterType { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the DrawerId.
        /// </summary>
        [Column]
        public int? DrawerId { get; set; }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        [Column]
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the CashRegisterCoinId.
        /// </summary>
        [Column]
        public int? CashRegisterCoinId { get; set; }

        /// <summary>
        /// Gets or sets the CashRegisterBillId.
        /// </summary>
        [Column]
        public int? CashRegisterBillId { get; set; }

        /// <summary>
        /// Gets or sets the CashRegisterRollId.
        /// </summary>
        [Column]
        public int? CashRegisterRollId { get; set; }

        /// <summary>
        /// Gets or sets the CashRegisterOtherId.
        /// </summary>
        [Column]
        public int? CashRegisterOtherId { get; set; }

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
