using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{
    public class Location
    {
        public long LocationId { get; set; }
        public long LocationType { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public bool IsFranchise { get; set; }
        public bool IsActive { get; set; }
        public string TaxRate { get; set; }
        public string SiteUrl { get; set; }
        public long Currency { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string WifiDetail { get; set; }
        public long WorkhourThreshold { get; set; }
    }
}
