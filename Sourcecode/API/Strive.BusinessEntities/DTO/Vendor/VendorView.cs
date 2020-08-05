using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Vendor
{
    public class VendorView : Vendor
    { 
       public List<VendorAddress> VendorAddress { get; set; }
    }
}
