using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblCashRegister")]
    public class CashRegister
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int CashRegisterId { get; set; }

        [Column]
        public int? CashRegisterType { get; set; }

        [Column]
        public int? LocationId { get; set; }

        [Column]
        public int? DrawerId { get; set; }

        [Column, PrimaryKey]
        public DateTime? CashRegisterDate { get; set; }

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
        public DateTime? StoreTimeIn { get; set; }


        [Column]
        public DateTime? StoreTimeOut { get; set; }


        [Column]
        public int? StoreOpenCloseStatus { get; set; }

        [Column]
        public decimal? Tips { get; set; }
        [Column]
        public decimal? TotalAmount { get; set; }

    }
}