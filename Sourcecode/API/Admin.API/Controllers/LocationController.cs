using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
using Strive.BusinessEntities.DTO.MembershipSetup;
using Strive.BusinessLogic.Location;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]

    [Route("Admin/[Controller]")]
    public class LocationController : StriveControllerBase<ILocationBpl>
    {
        public LocationController(ILocationBpl locBpl) : base(locBpl) { }

        #region POST
        [HttpPost]
        [Route("Add")]
        public Result AddLocation([FromBody] LocationDto location) => _bplManager.AddLocation(location);

        [HttpPost]
        [Route("Update")]
        public Result UpdateLocation([FromBody] LocationDto location) => _bplManager.UpdateLocation(location);

        //[HttpPost]
        //[Route("Save")]
        //public Result SaveLocation([FromBody]  LocationDto location) => _bplManager.SaveLocation(location);

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteLocation(int id) => _bplManager.DeleteLocation(id);

        [HttpPost]
        [Route("GetLocationSearch")]
        public Result GetLocationSearch([FromBody] LocationSearchDto search) => _bplManager.GetLocationSearch(search);

        [HttpPost]
        [Route("GetMerchantDetails")]
        public Result GetMerchantDetails([FromBody]MerchantSearch search) => _bplManager.GetMerchantDetails(search);



        #endregion

        #region GET
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllLocation() => _bplManager.GetAllLocation();

        [HttpGet]
        [Route("GetById")]
        public Result GetLocationById(int id) => _bplManager.GetLocationById(id);


        [HttpGet]
        [Route("GetAllLocationName")]
        public Result GetAllLocationName() => _bplManager.GetAllLocationName();

        #endregion
    }
}
