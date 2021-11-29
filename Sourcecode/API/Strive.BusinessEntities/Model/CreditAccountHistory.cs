using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblCreditAccountHistory")]
    public class CreditAccountHistory
    {
        [Column,PrimaryKey,IgnoreOnInsert,IgnoreOnUpdate]
        public int CreditAccountHistoryId { get; set; }

        [Column,PrimaryKey]
        public int CreditAccountId { get; set; }

        [Column]
        public decimal? Amount { get; set; }

        [Column]
        public string Comments { get; set; }

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

        [Column]
        public int? JobPaymentId { get; set; }

        [Column]
        public bool? TransactionType { get; set; }
    }
}
