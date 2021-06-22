using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblLocationAddress")]
    public class LocationAddress
    {

        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int LocationAddressId { get; set; }

        [Column]
        public int? LocationId { get; set; }

        [Column]
        public string Address1 { get; set; }

        [Column]
        public string Address2 { get; set; }

        [Column]
        public string PhoneNumber { get; set; }

        [Column]
        public string PhoneNumber2 { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public int? City { get; set; }

        [Column]
        public int? State { get; set; }

        [Column]
        public string Zip { get; set; }

        [Column]
        public int? Country { get; set; }

        [Column]
        public decimal? Longitude { get; set; }

        [Column]
        public decimal? Latitude { get; set; }

        [Column]
        public int? WeatherLocationId { get; set; }

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

        public string CityName { get; set; }
        public string StateName { get; set; }


    }
}