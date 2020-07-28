using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.Common;
using System.Collections.Generic;
using Strive.BusinessLogic.Location;
using Strive.BusinessEntities.Location;
using System.Linq;
using Admin.API.Helpers;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class LocationController : StriveControllerBase<ILocationBpl>
    {
        public LocationController(ILocationBpl locBpl) : base(locBpl) { }

        [HttpPost]
        [Route("Add")]
        public Result AddLocation([FromBody] LocationDto location) => _bplManager.AddLocation(location);

        [HttpPost]
        [Route("Update")]
        public Result UpdateLocation([FromBody] LocationDto location) => _bplManager.UpdateLocation(location);

        [HttpPost]
        [Route("Save")]
        public Result SaveLocation([FromBody]  LocationDto location) => _bplManager.SaveLocationDetails(location);

        [HttpDelete]
        [Route("{id}")]
        public Result DeleteLocation(int id) => _bplManager.DeleteLocationDetails(id);

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllLocation() => _bplManager.GetLocationDetails();

        [HttpGet]
        [Route("{id}")]
        public Result GeteLocationById(int id) => _bplManager.GetLocationById(id);


    }
}
