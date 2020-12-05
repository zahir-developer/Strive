using System;

namespace Strive.BusinessEntities.ViewModel
{
    public class LocationWashServiceViewModel
    {
        public int? LocationId { get; set; }
        public string LocationName { get; set; }
        public DateTime JobDate { get; set; }
        public int? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int WashCount { get; set; }
    }
}