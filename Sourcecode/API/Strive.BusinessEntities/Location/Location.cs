using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;
using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace Strive.BusinessEntities
{
    [Table("tblLocation")]
    public class Location
    {
        [Key]
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
        [Write(false)]
        public List<LocationAddress> LocationAddress { get; set; }
    }


}
