﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strive.BusinessEntities.Vendor
{
    public  class VendorAddress
    {
      public  int  AddressId { get; set; }
      public int RelationshipId { get; set; }
      public string Address1 { get; set; } 
      public string Address2 { get; set; }    
      public string PhoneNumber { get; set; }
      public string PhoneNumber2 { get; set; }
      public string Email { get; set; }
      public int City { get; set; }
      public int State { get; set; }
      public string  Zip { get; set; }
      public string Fax { get; set; }
      public bool IsActive { get; set; }
    
    }
}
