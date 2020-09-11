using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ClientVehicleMembershipView
    {
        public int MembershipId { get; set; }
        public string MembershipName { get; set; }
        public object Price { get; set; }
        public object Notes { get; set; }
        public int ServiceId { get; set; }
        public int LocationId { get; set; }
        public int ClientMembershipId { get; set; }
        public int ClientVehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }

    public class MembershipServiceView
    {
        public int MembershipServiceId { get; set; }
        public int MembershipId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public object ServiceType { get; set; }
        public string Upcharges { get; set; }
    }

    public class MembershipDetailsForVehicleId
    {
        public ClientVehicleMembershipView Membership { get; set; }
        public List<MembershipServiceView> MembershipService { get; set; }
    }

    public class ClientVehicleRootView
    {
        public MembershipDetailsForVehicleId MembershipDetailsForVehicleId { get; set; }
    }
}
