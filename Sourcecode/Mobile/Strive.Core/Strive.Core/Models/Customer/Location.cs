using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class Location
    {
        public int LocationId { get; set; }
        public int LocationAddressId { get; set; }
        public int LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LocationName { get; set; }
        public int WashTimeMinutes { get; set; }
        public decimal? Longitude { get; set; } 
        public decimal? Latitude { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WorkhourThreshold { get; set; }
        public bool IsActive { get; set; }
        public bool IsFranchise { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }
    public class Locations
    {
        public List<Location> Location { get; set; }
    }

    public class LocationStatus
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address1 { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int WashtimeMinutes { get; set; }
        public string? StoreStatus { get; set; }
        public string StoreTimeIn { get; set; }
        public string StoreTimeOut { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }

    public class washLocations
    {
        public List<LocationStatus> Washes { get; set; }
    }

    public class LocationStatusReq
    {
        public string Date { get; set; }
        public int LocationId { get; set; }
    }
}
