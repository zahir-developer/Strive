﻿using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class Location
    {
        public List<LocationAddress> LocationAddress { get; set; }
    }

    public class LocationProd
    {
        public List<LocationDetail> Location { get; set; }
    }
}
