using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblJobPaymentCreditCard")]
    public class JobPaymentCreditCard
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobPaymentCreditCardId { get; set; }

        [Column]
        public int JobPaymentId { get; set; }

        [Column]
        public int CardTypeId { get; set; }

        [Column]
        public int CardCategoryId { get; set; }

        [Column]
        public string CardNumber { get; set; }

        [Column]
        public int CreditCardTransactionTypeId { get; set; }

        [Column]
        public decimal? Amount { get; set; }

        [Column]
        public string TranRefNo { get; set; }

        [Column]
        public string TranRefDetails { get; set; }

        [Column]
        public bool? IsActive { get; set; }

        [Column]
        public bool? IsDeleted { get; set; }

        [Column]
        public int? CreatedBy { get; set; }

        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }

        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }

    }
}