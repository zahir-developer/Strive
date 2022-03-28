using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblJobPayment")]
    public class JobPayment
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobPaymentId { get; set; }

        [Column, PrimaryKey]
        public int? JobId { get; set; }

        [Column]
        public int? DrawerId { get; set; }

        [Column]
        public decimal? Amount { get; set; }

        [Column]
        public decimal? TaxAmount { get; set; }

        [Column]
        public bool? Approval { get; set; }

        [Column]
        public int? PaymentStatus { get; set; }

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
        public  DateTimeOffset? UpdatedDate { get; set; }

        [Column]
        public bool? IsProcessed { get; set; }
        [Column]
        public int? MembershipId { get; set; }
        [Column]
        public decimal? CashBack { get; set; }

    }
}