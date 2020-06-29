using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class LocationController : ControllerBase
    {

        ILocationBpl locationBpl;

        public LocationController(ILocationBpl _locationBpl)
        {
            locationBpl = _locationBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllLocation()
        {
            return locationBpl.GetAllLocation();
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveLocation(List<Location> locations)
        {
            return locationBpl.SaveLocation(locations);
        }
    }
}