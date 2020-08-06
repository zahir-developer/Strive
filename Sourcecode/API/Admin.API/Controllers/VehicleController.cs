using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.MembershipSetup;
using Strive.BusinessLogic.Vehicle;
using Strive.Common;
using System.Collections.Generic;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class VehicleController : StriveControllerBase<IVehicleBpl>
    {
        public VehicleController(IVehicleBpl vehicleBpl) : base(vehicleBpl) { }

        [HttpPost]
        [Route("Update")]
        public Result UpdateVehicle([FromBody] VehicleDto vehicle) => _bplManager.SaveClientVehicle(vehicle);

        [HttpPost]
        [Route("UpdateMembership")]
        public Result Update([FromBody] Membership Membership) => _bplManager.UpdateVehicleMembership(Membership);

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteVehicle(int id) => _bplManager.DeleteVehicle(id);


        [HttpGet]
        [Route("GetVehicleById")]
        public Result GetVehicleById(int id) => _bplManager.GetClientVehicleById(id);

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllVehicle()
        {
            return _bplManager.GetAllVehicle();
        }

        [HttpGet]
        [Route("GetVehicleMembership")]
        public Result GetVehicleMembership()
        {
            return _bplManager.GetVehicleMembership();
        }

    }
}