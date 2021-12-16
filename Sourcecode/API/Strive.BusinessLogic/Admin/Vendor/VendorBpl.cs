using Strive.Common;
using System;
using System.Collections.Generic;
using System.Net;
using Strive.BusinessEntities.Vendor;
using Strive.ResourceAccess;
using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.Vendor;

namespace Strive.BusinessLogic
{
    public class VendorBpl : Strivebase, IVendorBpl
    {
        public VendorBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result GetVendorDetails()
        {
            return ResultWrap(new VendorRal(_tenant).GetVendorDetails, "Vendor");
        }
        public Result AddVendor(VendorDTO vendor)
        {
            return ResultWrap(new VendorRal(_tenant).AddVendor, vendor, "Status");
        }

        public Result UpdateVendor(VendorDTO vendor)
        {
            return ResultWrap(new VendorRal(_tenant).UpdateVendor, vendor, "Status");
        }
        public Result DeleteVendorById(int id)
        {
            return ResultWrap(new VendorRal(_tenant).DeleteVendorById, id, "Vendor");
        }

        public Result GetVendorById(int id)
        {
            return ResultWrap(new VendorRal(_tenant).GetVendorById, id, "VendorDetail");
        }

        public Result GetVendorByIds(string ids)
        {
            return ResultWrap(new VendorRal(_tenant).GetVendorByIds, ids, "VendorDetail");
        }

        public Result GetVendorSearch(VendorSearchDto search)
        {
            return ResultWrap(new VendorRal(_tenant).GetVendorSearch, search, "VendorSearch");
        }

        public Result GetAllVendorName()
        {
            return ResultWrap(new VendorRal(_tenant).GetAllVendorName, "Vendor");
        }

        public Result GetVendorByProductId(int id)
        {
            return ResultWrap(new VendorRal(_tenant).GetVendorByProductId,id, "VendorProduct");
        }
    }
}
