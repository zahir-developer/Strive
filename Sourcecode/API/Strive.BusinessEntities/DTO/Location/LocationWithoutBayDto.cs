using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Location
{
    public class LocationWithoutBayDto
    {
        public Model.Location Location { get; set; }
        public LocationAddress LocationAddress { get; set; }
        public Drawer Drawer { get; set; }
    }
}
