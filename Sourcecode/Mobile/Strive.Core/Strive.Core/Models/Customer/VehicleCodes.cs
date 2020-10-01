using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class VehicleCodes
    {
        public List<VehicleDetails> VehicleDetails { get; set; }
    }
    public class VehicleDetails
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int CodeId { get; set; }
        public string CodeValue { get; set; }
    }
}
