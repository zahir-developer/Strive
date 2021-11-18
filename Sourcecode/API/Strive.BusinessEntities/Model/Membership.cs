using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblMembership")]
    public class Membership
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int MembershipId { get; set; }

        [Column]
        public string MembershipName { get; set; }

        [Column]
        public int? LocationId { get; set; }

        [Column]
        public decimal? Price { get; set; }

        [Column]
        public string Notes { get; set; }

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
        public decimal? DiscountedPrice { get; set; }

    }
}