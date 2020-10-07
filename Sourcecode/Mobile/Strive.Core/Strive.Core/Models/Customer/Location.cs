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
}
