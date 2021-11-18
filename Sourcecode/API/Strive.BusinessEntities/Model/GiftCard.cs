using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblGiftCard")]
    public class GiftCard
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int? ClientId { get; set; }

        [Column]
        public int GiftCardId { get; set; }

        [Column]
        public int? LocationId { get; set; }

        [Column]
        public string GiftCardCode { get; set; }

        [Column]
        public string GiftCardName { get; set; }

        [Column]
        public DateTimeOffset ActivationDate { get; set; }
        [Column]
        public decimal TotalAmount { get; set; }
        [Column]
        public decimal BalanceAmount { get; set; }
        [Column]
        public string Comments { get; set; }

        [Column]
        public string Email { get; set; }

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