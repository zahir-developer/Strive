using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblCreditAccount")]
    public class CreditAccount
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int CreditAccountId { get; set; }

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ClientId { get; set; }

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
    }
}
