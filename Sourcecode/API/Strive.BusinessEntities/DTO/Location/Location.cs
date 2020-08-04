using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Location
{
    public class Location
    {
        public int LocationId { get; set; }
        public int? LocationType { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public bool? IsFranchise { get; set; }
        public bool? IsActive { get; set; }
        public string TaxRate { get; set; }
        public string SiteUrl { get; set; }
        public int? Currency { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string WifiDetail { get; set; }
        public int? WorkhourThreshold { get; set; }
    }
}
