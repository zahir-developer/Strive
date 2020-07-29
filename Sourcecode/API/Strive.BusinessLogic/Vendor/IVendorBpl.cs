using System.Collections.Generic;
using Strive.BusinessEntities.Vendor;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IVendorBpl
    {
        Result GetVendorDetails();
        Result SaveVendorDetails(List<VendorList> lstVendor);
        Result DeleteVendorById(int id);
        Result GetVendorById(long id);
        Result AddVendor(VendorDTO vendor);
    }
}
