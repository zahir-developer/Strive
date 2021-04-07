using Strive.BusinessEntities.Model;
using System.Collections.Generic;
using Model = Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.Vendor
{
    public class VendorDTO
    {
        public Model.Vendor Vendor { get; set; }
        public Model.VendorAddress VendorAddress { get; set; }
        public List<VendorEmailAddress> VendorEmailAddress { get; set; }
    }
}
