using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strive.BusinessEntities.Vendor
{
    public class Vendor
    {
       public int VendorId  {get; set;}
       public string VIN { get; set; }
       public string VendorName { get; set; }
       public string VendorAlias { get; set; }
       public bool IsActive { get; set; }
       
    }
}
