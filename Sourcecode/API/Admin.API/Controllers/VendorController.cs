using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using Strive.BusinessEntities.Vendor;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class VendorController
    {
        IVendorBpl _vendorBpl = null;

        public VendorController(IVendorBpl vendorBpl)
        {
            _vendorBpl = vendorBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllVendor()
        {
            return _vendorBpl.GetVendorDetails();
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveTodayCashRegister([FromBody] List<Strive.BusinessEntities.Vendor.VendorList> lstVendor)
        {
            return _vendorBpl.SaveVendorDetails(lstVendor);
        }


    }
}
