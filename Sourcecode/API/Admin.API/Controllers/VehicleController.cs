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
        [HttpPost]
        [Route("UpdateVehicle")]
        public Result UpdateVehicleById([FromBody] Strive.BusinessEntities.Client.ClientVehicle lstUpdateVehicle)
        {
            return _IVehicleBpl.UpdateClientVehicle(lstUpdateVehicle);
        }
        [HttpDelete]
        [Route("id")]
        public Result DeleteVehicleById(int id)
        {
            return _IVehicleBpl.DeleteVehicle(id);
        }
        [HttpGet]
        [Route("GetVehicleById/{id}")]
        public Result GetVehicleById(int id)
        {
            return _IVehicleBpl.GetClientVehicleById(id);
        }
    }
}