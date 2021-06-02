using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Vendor;
using Strive.BusinessEntities.Vendor;
using Strive.BusinessLogic;
using Strive.Common;
using System.Collections.Generic;


namespace Admin.API.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]

    [Route("Admin/[Controller]")]
    public class VendorController : StriveControllerBase<IVendorBpl>
    {
        public VendorController(IVendorBpl vendorBpl) : base(vendorBpl) { }

       
        [HttpPost]
        [Route("Add")]
        public Result AddVendor([FromBody] VendorDTO vendor) => _bplManager.AddVendor(vendor);
        
        [HttpPost]
        [Route("Update")]
        public Result UpdateVendor([FromBody] VendorDTO vendorn) => _bplManager.UpdateVendor(vendorn);
       
        [HttpDelete]
        [Route("Delete/{id}")]
        public Result DeleteVendorById(int id) => _bplManager.DeleteVendorById(id);
       
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllVendor() => _bplManager.GetVendorDetails();


        [HttpGet]
        [Route("GetAllVendorName")]
        public Result GetAllVendorName() => _bplManager.GetAllVendorName();
        
        [HttpGet]
        [Route("GetVendorById/{id}")]
        public Result GetVendorById(int id) => _bplManager.GetVendorById(id);

        [HttpGet]
        [Route("GetVendorByIds/{ids}")]
        public Result GetVendorByIds(string ids) => _bplManager.GetVendorByIds(ids);

        [HttpPost]
        [Route("GetVendorSearch")]
        public Result GetVendorSearch([FromBody] VendorSearchDto search) => _bplManager.GetVendorSearch(search);


        [HttpGet]
        [Route("GetVendorByProductId/{id}")]
        public Result GetVendorByProductId(int id) => _bplManager.GetVendorByProductId(id);

    }
}
