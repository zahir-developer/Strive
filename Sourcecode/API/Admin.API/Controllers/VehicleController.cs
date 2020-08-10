using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.Model;
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
        [Route("GetVehicleById")]
        public Result GetVehicleById(int id) => _bplManager.GetClientVehicleById(id);

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
        [HttpGet]
        [Route("GetCodeTypeColour")]
        public Result GetVehicleColour() => _bplManager.GetVehicleColour();
        [HttpGet]
        [Route("GetCodeTypeModel")]
        public Result GetCodeTypeModel() => _bplManager.GetCodeTypeModel();
        [HttpGet]
        [Route("GetCodeModel")]
        public Result GetCodeModel() => _bplManager.GetCodeModel();
        [HttpGet]
        [Route("GetCodeUpcharge")]
        public Result GetCodeUpcharge() => _bplManager.GetCodeUpcharge();
        [HttpGet]
        [Route("GetCodeMake")]
        public Result GetCodeMake() => _bplManager.GetCodeMake();
        
    }
}