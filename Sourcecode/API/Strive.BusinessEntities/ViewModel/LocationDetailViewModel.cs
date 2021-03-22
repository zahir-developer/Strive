using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class LocationDetailViewModel
    {
        public int LocationId { get; set; }
        public int? LocationType { get; set; }
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public string ColorCode { get; set; }
        public int? WashTimeMinutes { get; set; }
        public bool? IsFranchise { get; set; }
        public string TaxRate { get; set; }
        public string SiteUrl { get; set; }
        public int? Currency { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string WifiDetail { get; set; }
        public decimal? WorkhourThreshold { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }
}
