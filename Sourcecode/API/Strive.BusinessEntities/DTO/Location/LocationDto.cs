using Strive.BusinessEntities.Model;
using System.Collections.Generic;

namespace Strive.BusinessEntities.DTO
{
    public class LocationDto
    {
        public Model.Location Location { get; set; }
        public List<LocationEmail> LocationEmail  { get; set; }
        public LocationAddress LocationAddress { get; set; }
        public Drawer Drawer { get; set; }
        public List<Bay> Bay { get; set; }
        public LocationOffset LocationOffset { get; set; }
        public MerchantDetail MerchantDetail { get; set; }

    }
}
