using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Strive.BusinessEntities
{
    public class Location
    {
        public int LocationId { get; set; }
        public int? LocationType { get; set; }
        [MaxLength(100)]
        public string LocationName { get; set; }
        [MaxLength(200)]
        public string LocationDescription { get; set; }
        public bool? IsFranchise { get; set; }
        public bool? IsActive { get; set; }
        [MaxLength(50)]
        public string TaxRate { get; set; }
        [MaxLength(100)]
        public string SiteUrl { get; set; }
        public int? Currency { get; set; }
        [MaxLength(200)]
        public string Facebook { get; set; }
        [MaxLength(200)]
        public string Twitter { get; set; }
        [MaxLength(200)]
        public string Instagram { get; set; }
        [MaxLength(100)]
        public string WifiDetail { get; set; }
        public int? WorkhourThreshold { get; set; }

        public List<LocationAddress> LocationAddress { get; set; }
    }


}
