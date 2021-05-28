using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblJobPaymentDetail")]
    public class JobPaymentDetail
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int JobPaymentDetailId { get; set; }

        [Column, PrimaryKey]
        public int? JobPaymentId { get; set; }

        [Column]
        public int? PaymentType { get; set; }

        [Column]
        public decimal? Amount { get; set; }

        [Column]
        public decimal? TaxAmount { get; set; }

        [Column]
        public string Signature { get; set; }
        [Column]
        public int? ReferenceNumber { get; set; }

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
