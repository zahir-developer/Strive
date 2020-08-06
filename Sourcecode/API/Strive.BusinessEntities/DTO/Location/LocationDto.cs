using Strive.BusinessEntities.Model;
using System.Collections.Generic;

namespace Strive.BusinessEntities.DTO
{
    public class LocationDto
    {
        public Model.Location Location { get; set; }
        public List<LocationAddress> LocationAddress { get; set; }
        public Drawer Drawer { get; set; }
    }
}
