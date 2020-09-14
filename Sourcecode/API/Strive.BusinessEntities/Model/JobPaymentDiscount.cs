using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblJobPaymentDiscount")]
    public class JobPaymentDiscount
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobPaymentDiscountId { get; set; }

        [Column]
        public int JobPaymentId { get; set; }

        [Column]
        public int ServiceDiscountId { get; set; }

        [Column]
        public decimal? Amount { get; set; }

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