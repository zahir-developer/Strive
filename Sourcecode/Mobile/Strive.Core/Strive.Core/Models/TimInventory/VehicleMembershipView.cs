using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ClientVehicleView
    {
        public int VehicleId { get; set; }
        public int ClientId { get; set; }
        public int LocationId { get; set; }
        public string VehicleNumber { get; set; }
        public int VehicleMfr { get; set; }
        public int VehicleModel { get; set; }
        public int VehicleModelNo { get; set; }
        public string VehicleYear { get; set; }
        public int VehicleColor { get; set; }
        public int Upcharge { get; set; }
        public string Barcode { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public object MonthlyCharge { get; set; }
    }

    public class ClientVehicleMembershipView
    {
        public int ClientMembershipId { get; set; }
        public int ClientVehicleId { get; set; }
        public int LocationId { get; set; }
        public int MembershipId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }

    public class ClientVehicleMembershipServiceView
    {
        public int ClientVehicleMembershipServiceId { get; set; }
        public int ClientMembershipId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedDate { get; set; }
        public object UpdatedBy { get; set; }
        public object UpdatedDate { get; set; }
    }

    public class VehicleMembershipDetails
    {
        public ClientVehicleView ClientVehicle { get; set; }
        public ClientVehicleMembershipView ClientVehicleMembership { get; set; }
        public List<ClientVehicleMembershipServiceView> ClientVehicleMembershipService { get; set; }
    }

    public class ClientVehicleRootView
    {
        public VehicleMembershipDetails VehicleMembershipDetails { get; set; }
    }
}
