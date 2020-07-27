using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.Common;
using System.Collections.Generic;
using Strive.BusinessLogic.Location;
using Strive.BusinessEntities.Location;
using System.Linq;

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
        public Result SaveLocation([FromBody]  List<LocationView> lstLocation)
        {
            return _locationBpl.SaveLocationDetails(lstLocation.FirstOrDefault());
        }
        [HttpDelete]
        [Route("{id}")]
        public Result DeleteLocation(int id)
        {
            return _locationBpl.DeleteLocationDetails(id);
        }

        [HttpGet]
        [Route("{id}")]
        public Result GeteLocationById(int id)
        {
            return _locationBpl.GetLocationById(id);
        }
        [HttpPost]
        [Route("Add")]
        public Result AddLocation([FromBody] List<LocationView> lstLocation)
        {
            return _locationBpl.AddLocation(lstLocation);
        }

        [HttpPost]
        [Route("Update")]
        public Result UpdateLocation([FromBody] List<LocationView> lstLocation)
        {
            return _locationBpl.UpdateLocation(lstLocation);
        }
        [HttpGet]
        [Route("GetAllLocationAddress")]
        public Result GetAllLocationAddress()
        {
            return _locationBpl.GetAllLocationAddress();
        }
    }
}
