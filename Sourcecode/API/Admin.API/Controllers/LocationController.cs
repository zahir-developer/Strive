using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
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
        [Route("AddLocationOffset")]
        public Result AddLocationOffset([FromBody] LocationOffsetDto locationOffset) => _bplManager.AddLocationOffset(locationOffset);
        [HttpPost]
        [Route("UpdateLocationOffset")]
        public Result UpdateLocationOffset([FromBody] LocationOffsetDto locationOffset) => _bplManager.UpdateLocationOffset(locationOffset);


        [HttpDelete]
        [Route("DeleteLocationOffset")]
        public Result DeleteLocationOffset(int id) => _bplManager.DeleteLocationOffset(id);

        #endregion

        #region GET
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllLocation() => _bplManager.GetAllLocation();

        [HttpGet]
        [Route("GetById")]
        public Result GetLocationById(int id) => _bplManager.GetLocationById(id);
        [HttpGet]
        [Route("GetAllLocationOffset")]
        public Result GetAllLocationOffset() => _bplManager.GetAllLocationOffset();

        #endregion
    }
}
