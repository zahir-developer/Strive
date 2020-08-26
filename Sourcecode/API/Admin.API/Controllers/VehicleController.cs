using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
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

        [HttpPost]
        [Route("UpdateClientVehicle")]
        public Result UpdateClientVehicle([FromBody] ClientVehicle ClientVehicle) => _bplManager.UpdateClientVehicle(ClientVehicle);

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteVehicle(int id) => _bplManager.DeleteVehicle(id);


        [HttpGet]
        [Route("GetVehicleByClientId")]
        public Result GetVehicleByClientId(int id) => _bplManager.GetVehicleByClientId(id);

        [HttpGet]
        [Route("GetVehicleId")]
        public Result GetVehicleId(int id) => _bplManager.GetVehicleId(id);

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
        [HttpPost]
        [Route("GetVehicleCodes")]
        public Result GetVehicleCodes() => _bplManager.GetVehicleCodes();

        [HttpPost]
        [Route("SaveClientVehicleMembership")]
        public Result SaveClientMembership([FromBody] VehicleMembershipViewModel clientmembership) => _bplManager.SaveClientVehicleMembership(clientmembership);
        
    }
}