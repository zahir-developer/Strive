using System.Collections.Generic;
using Strive.BusinessEntities.DTO.Vendor;
using Strive.BusinessEntities.Vendor;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IVendorBpl
    {
        Result GetVendorDetails();
        Result AddVendor(VendorDTO vendor);
        Result UpdateVendor(VendorDTO vendor);
        Result DeleteVendorById(int id);
        Result GetVendorById(int id);
        Result GetVendorByIds(string ids);
        Result GetVendorSearch(VendorSearchDto search);
        Result GetAllVendorName();
        Result GetVendorByProductId(int id);
    }
}
