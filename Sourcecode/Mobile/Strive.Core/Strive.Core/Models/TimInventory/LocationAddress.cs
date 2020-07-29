using System;
namespace Strive.Core.Models.TimInventory
{
    public class LocationAddress
    {
        public int AddressId { get; set; }
        public int RelationshipId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
        public int Country { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string WashTiming { get; set; }
        public string WeatherLocationId { get; set; }

    }
}
