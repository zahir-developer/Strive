using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class VendorDetail
    {
        public int VendorId { get; set; }
        public string VIN { get; set; }
        public string VendorName { get; set; }
        public string VendorAlias { get; set; }
        public string IsActive { get; set; }
        public int VendorAddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public string Zip { get; set; }
        public string Fax { get; set; }
        public int Country { get; set; }
    }

    public class Vendors
    {
        public List<VendorDetail> Vendor { get; set; }
    }

}
