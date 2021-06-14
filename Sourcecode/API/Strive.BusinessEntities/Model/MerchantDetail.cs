using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblMerchantDetail")]
    public class MerchantDetail
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int MerchantDetailId { get; set; }

        [Column]
        public int? LocationId { get; set; }

        [Column]
        public int? MID { get; set; }

        [Column]
        public string UserName { get; set; }

        [Column]
        public string Password { get; set; }
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