using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblEmployeeLiability")]
    public class EmployeeLiability
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int LiabilityId { get; set; }

        [Column, PrimaryKey]
        public int? EmployeeId { get; set; }

        [Column]
        public int LiabilityType { get; set; }

        [Column]
        public string LiabilityDescription { get; set; }

        [Column]
        public int? ProductId { get; set; }

        [Column]
        public decimal? TotalAmount { get; set; }

        [Column]
        public int? Status { get; set; }

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

        [Column, PrimaryKey]
        public int? VehicleId { get; set; }

        [Column, PrimaryKey]
        public int? ClientId { get; set; }

        [Column]
        public int? LocationId { get; set; }

    }
}