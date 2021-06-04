using System;
namespace Strive.Core.Models.TimInventory
{
    public class LocationAddress
    {        
        public int LocationId { get; set; }
        public int LocationAddressId { get; set; }
        public int LocationTypeId { get; set; }
        public string? LocationTypeName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string LocationName { get; set; }
        public int WashTimeMinutes { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public double? WorkhourThreshold { get; set; }
        public bool IsActive { get; set; }
        public bool IsFranchise { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

    }

    public class LocationDetail
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int? WashTimeMinutes { get; set; }
    }
}
