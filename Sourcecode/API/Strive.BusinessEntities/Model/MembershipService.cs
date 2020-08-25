using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblMembershipService")]
    public class MembershipService
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int MembershipServiceId { get; set; }

        [Column]
        public int? MembershipId { get; set; }

        [Column]
        public int? ServiceId { get; set; }

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