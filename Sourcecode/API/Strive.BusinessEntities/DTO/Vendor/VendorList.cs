using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Vendor
{
    public class VendorList
    { 
        public int VendorId { get; set; }
        public string VIN { get; set; }
        public string VendorName { get; set; }
        public string VendorAlias { get; set; }
        public bool IsActive { get; set; }
       public List<VendorAddress> VendorAddress { get; set; }
    }
}
