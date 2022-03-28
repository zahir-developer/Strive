using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblClientVehicleMembershipDetails")]
    public class ClientVehicleMembershipDetails
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int ClientMembershipId { get; set; }

        [Column]
        public int ClientVehicleId { get; set; }

        [Column]
        public int LocationId { get; set; }

        [Column]
        public int MembershipId { get; set; }

        [Column]
        public DateTime? StartDate { get; set; }

        [Column]
        public DateTime? EndDate { get; set; }

        [Column]
        public int? DocumentId { get; set; }

        [Column]
        public bool? Status { get; set; }

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
        public decimal? TotalPrice { get; set; }
        [Column]
        public bool? IsDiscount { get; set; }

        [Column]
        public string CardNumber { get; set; }
        [Column]
        public string ExpiryDate { get; set; }
        [Column]
        public string ProfileId { get; set; }
        [Column]
        public string AccountId { get; set; }
        [Column]
        public int? FailedAttempts { get; set; }
        [Column]
        public bool? IsNotified { get; set; }


    }
}