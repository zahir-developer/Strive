using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace Strive.BusinessEntities.Location
{
    [Table("tblLocationAddress")]
    public class LocationAddressModel
    {

        [Key]
        public int AddressId { get; set; }
        public int RelationshipId { get; set; }
        [MaxLength(100)]
        public string Address1 { get; set; }
        [MaxLength(100)]
        public string Address2 { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string PhoneNumber2 { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        [MaxLength(100)]
        public string Zip { get; set; }
        public bool IsActive { get; set; }
        public int Country { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        [MaxLength(100)]
        public string WeatherLocationId { get; set; }

        public LocationAddressModel()
        {

        }

    }
}
