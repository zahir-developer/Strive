using Model = Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.Vendor
{
    public class VendorDTO
    {
        public Model.Vendor Vendor { get; set; }
        public Model.VendorAddress VendorAddress { get; set; }
    }
}
