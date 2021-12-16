using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblClientVehicleMembershipService")]
    public class ClientVehicleMembershipService
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ClientVehicleMembershipServiceId { get; set; }

        [Column, PrimaryKey]
        public int? ClientMembershipId { get; set; }

        [Column, PrimaryKey]
        public int? ServiceId { get; set; }

        [Ignore]
        public int? ServiceTypeId { get; set; }

        [Ignore]
        public string ServiceType { get; set; }

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