using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblLocationEmail")]
    public class LocationEmail
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int LocationEmailId { get; set; }

        [Column]
        public int? LocationId { get; set; }

        [Column]
        public string EmailAddress { get; set; }

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