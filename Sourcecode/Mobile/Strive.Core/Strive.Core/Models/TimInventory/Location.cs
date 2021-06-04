using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class Locations
    {
        public List<LocationAddress> Location { get; set; }
    }

    public class LocationProd
    {
        public List<LocationDetail> Location { get; set; }
    }
}
