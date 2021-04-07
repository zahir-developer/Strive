using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblVendorEmailAddress")]
    public class VendorEmailAddress
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int VendorEmailAddressId { get; set; }

        [Column]
        public int VendorId { get; set; }

        [Column]
        public string VendorEmail { get; set; }

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

    }
}