using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class LocationController : ControllerBase
    {
        ILocationBpl _locationBpl = null;

        public LocationController(ILocationBpl locationBpl)
        {
            _locationBpl = locationBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAlllocation()
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

        [HttpGet]
        [Route("{id}")]
        public Result GeteLocationById(int id)
        {
            return _locationBpl.GetLocationById(id);
        }


    }
}
