using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblVehicleIssue")]
    public class VehicleIssue
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int VehicleIssueId { get; set; }

        [Column]
        public int VehicleId { get; set; }

        [Column]
        public string Description { get; set; }

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