using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Vendor;
using Strive.BusinessLogic;
using Strive.Common;
using System.Collections.Generic;


namespace Admin.API.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]

    [Route("Admin/[Controller]")]
    public class VendorController : StriveControllerBase<IVendorBpl>
    {
        public VendorController(IVendorBpl vendorBpl) : base(vendorBpl) { }
      
        [HttpPost]
        [Route("Add")]
        public Result AddVendor([FromBody] VendorDTO vendor)
        {
            return _bplManager.AddVendor(vendor);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public Result DeleteVendorById(int id)
        {
            return _bplManager.DeleteVendorById(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllVendor()
        {
            return _bplManager.GetVendorDetails();
        }

        [HttpGet]
        [Route("GetVendorById/{id}")]
        public Result GetCollisionById(long id)
        {
            return _bplManager.GetVendorById(id);
        }
    }
}
