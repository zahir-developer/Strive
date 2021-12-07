using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ClientVehicleDetail
    {
        public int vehicleId { get; set; }
        public int clientId { get; set; }
        public int locationId { get; set; }
        public string vehicleNumber { get; set; }
        public int vehicleMfr { get; set; }
        public int? vehicleModel { get; set; }
        public int? vehicleModelNo { get; set; }
        public string vehicleYear { get; set; }
        public int vehicleColor { get; set; }
        public int? upcharge { get; set; }
        public string barcode { get; set; }
        public string notes { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
        public int monthlyCharge { get; set; }
    }

    public class ClientVehicle
    {
        public ClientVehicleDetail clientVehicle { get; set; }
    }

    public class ClientVehicleMembershipDetails
    {
        public int clientMembershipId { get; set; }
        public int clientVehicleId { get; set; }
        public int locationId { get; set; }
        public int membershipId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool status { get; set; }
        public string notes { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }
    }

    public class ClientVehicleMembershipService
    {
        public int clientVehicleMembershipServiceId { get; set; }
        public int clientMembershipId { get; set; }
        public int? serviceId { get; set; }
        public int? serviceTypeId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }
        public string services { get; set; }
    }

    public class ClientVehicleMembershipModel
    {
        public ClientVehicleMembershipDetails clientVehicleMembershipDetails { get; set; }
        public List<ClientVehicleMembershipService> clientVehicleMembershipService { get; set; }
    }

    public class ClientVehicleRoot
    {
        public ClientVehicle clientVehicle { get; set; }
        public ClientVehicleMembershipModel clientVehicleMembershipModel { get; set; }
    }

    public class VehicleDiscountDetail
    {
        
        public string Status { get; set; }
        
    }
    

}
