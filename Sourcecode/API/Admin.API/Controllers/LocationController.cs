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

    [Route("Admin/[Controller]")]
    public class LocationController : StriveControllerBase<ILocationBpl>
    {
        public LocationController(ILocationBpl locBpl) : base(locBpl) { }

        #region POST
        /// <summary>
        /// Add Location,LocationAddress,Drawer and Bay 
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Result AddLocation([FromBody] LocationDto location) => _bplManager.AddLocation(location);

        /// <summary>
        /// Update Location,LocationAddress,Drawer and Bay 
        /// </summary>
        [HttpPost]
        [Route("Update")]
        public Result UpdateLocation([FromBody] LocationDto location) => _bplManager.UpdateLocation(location);
        #endregion

        #region DELETE
        /// <summary>
        /// Delete Location Details By Given LocationId
        /// </summary>
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteLocation(int id) => _bplManager.DeleteLocation(id);
        #endregion

        #region GET
        /// <summary>
        /// Show search result in Grid by search params
        /// </summary>
        [HttpPost]
        [Route("GetSearchResult")]
        public Result GetLocationSearch([FromBody] LocationSearchDto search) => _bplManager.GetLocationSearch(search);
        
        /// <summary>
        /// To show all Location details in Grid
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllLocation() => _bplManager.GetAllLocation();

        /// <summary>
        /// To show Location Details for Given LocationId
        /// </summary>
        [HttpGet]
        [Route("GetById")]
        public Result GetLocationById(int id) => _bplManager.GetLocationById(id); 
        #endregion
    }
}
