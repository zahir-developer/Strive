using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic.Vehicle;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class VehicleController : ControllerBase
    {
        readonly IVehicleBpl _IVehicleBpl = null;

        public VehicleController(IVehicleBpl vehicleBpl)
        {
            _IVehicleBpl = vehicleBpl;
        }
        [HttpGet]
        [Route("GetAllVehicle")]
        public Result GetAllVehicle()
        {
            return _IVehicleBpl.GetAllVehicle();
        }
    }
}