using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.Common;
using System.Collections.Generic;
using Strive.BusinessLogic.Location;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class LocationController : ControllerBase
    {
        readonly ILocationBpl _locationBpl = null;

        public LocationController(ILocationBpl locationBpl)
        {
            _locationBpl = locationBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllLocation()
        {
            return _locationBpl.GetLocationDetails();
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveLocation([FromBody] List<Strive.BusinessEntities.Location> lstLocation)
        {
            return _locationBpl.SaveLocationDetails(lstLocation);
        }
        [HttpDelete]
        [Route("{id}")]
        public Result DeleteLocation(int id)
        {
            return _locationBpl.DeleteLocationDetails(id);
        }


    }
}
