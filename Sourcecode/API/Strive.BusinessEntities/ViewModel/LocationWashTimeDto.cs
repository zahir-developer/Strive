﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class LocationWashTimeDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int WashtimeMinutes { get; set; }
    }
}
