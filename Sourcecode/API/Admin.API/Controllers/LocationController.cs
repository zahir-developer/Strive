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
        /// Method to Add location and address details.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Result AddLocation([FromBody] LocationDto location) => _bplManager.AddLocation(location);

        /// <summary>
        /// Method to Update location and address details.
        /// </summary>
        [HttpPost]
        [Route("Update")]
        public Result UpdateLocation([FromBody] LocationDto location) => _bplManager.UpdateLocation(location);

        /// <summary>
        /// Retrieves list of all locations by search params
        /// </summary>
        [HttpPost]
        [Route("Search")]
        public Result GetSearchResult([FromBody] LocationSearchDto search) => _bplManager.GetSearchResult(search);
        #endregion

        #region DELETE
        /// <summary>
        /// Deletes a location based on the given locationId.
        /// </summary>
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteLocation(int id) => _bplManager.DeleteLocation(id);
        #endregion

        #region GET
        /// <summary>
        /// Retrieves list of all locations available.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllLocation() => _bplManager.GetAllLocation();

        /// <summary>
        /// Method to retrieve location details based on given locationId.
        /// </summary>
        [HttpGet]
        [Route("GetById")]
        public Result GetLocationById(int id) => _bplManager.GetLocationById(id); 
        #endregion
    }
}
