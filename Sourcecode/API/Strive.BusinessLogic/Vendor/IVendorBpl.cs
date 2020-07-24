using System.Collections.Generic;
using Strive.BusinessEntities.Vendor;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IVendorBpl
    {
        Result GetVendorDetails();
        Result SaveVendorDetails(VendorView lstVendor);
        Result DeleteVendorById(int id);
        Result GetVendorById(long id);
    }
}
