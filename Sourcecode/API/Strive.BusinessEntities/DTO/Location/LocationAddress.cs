using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Location
{
    [Table("tblLocationAddress")]
    public class LocationAddress
    {
        [Key]
        public int LocationAddressId { get; set; }        
        public int LocationId { get; set; }
        
        public string Address1 { get; set; }
                
        public string Address2 { get; set; }
                
        public string PhoneNumber { get; set; }
                
        public string PhoneNumber2 { get; set; }
               
        public string Email { get; set; }
                
        public int? City { get; set; }
              
        public int? State { get; set; }
                
        public string Zip { get; set; }
            
        public int? Country { get; set; }
                
        public decimal? Longitude { get; set; }
                
        public decimal? Latitude { get; set; }
                
        public int? WeatherLocationId { get; set; }
               
        public bool IsActive { get; set; }
                
        public bool IsDeleted { get; set; }
              
        public int? CreatedBy { get; set; }
                
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
