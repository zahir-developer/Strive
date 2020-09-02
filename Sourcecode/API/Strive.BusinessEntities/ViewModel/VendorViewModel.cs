using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class VendorViewModel
    {
        public int VendorId { get; set; }
        public string VIN { get; set; }
        public string VendorName { get; set; }
        public string VendorAlias { get; set; }
        public string IsActive { get; set; }
        public string websiteAddress { get; set; }
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
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
    }
}
