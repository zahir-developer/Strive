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
        [Route("UpdateMembership")]
        public Result Update([FromBody] Membership Membership) => _bplManager.UpdateVehicleMembership(Membership);

        [HttpPost]
        [Route("AddVehicle")]
        public Result AddVehicle([FromBody] VehicleDto ClientVehicle) => _bplManager.AddVehicle(ClientVehicle);

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteVehicle(int id) => _bplManager.DeleteVehicle(id);


        [HttpGet]
        [Route("GetVehicleByClientId")]
        public Result GetVehicleByClientId(int id) => _bplManager.GetVehicleByClientId(id);

        [HttpGet]
        [Route("GetVehicleId")]
        public Result GetVehicleId(int id) => _bplManager.GetVehicleId(id);

        [HttpPost]
        [Route("GetAll")]
        public Result GetAllVehicle([FromBody]VehicleSearchDto name)
        {
            return _bplManager.GetAllVehicle(name);
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
        public Result SaveClientMembership([FromBody] ClientVehicleMembershipDetailModel clientmembership) => _bplManager.SaveClientVehicleMembership(clientmembership);


        [HttpGet]
        [Route("GetVehicleMembershipDetailsByVehicleId")]
        public Result GetVehicleMembershipDetailsByVehicleId(int id) => _bplManager.GetVehicleMembershipDetailsByVehicleId(id);

        [HttpGet]
        [Route("GetMembershipDetailsByVehicleId")]
        public Result GetMembershipDetailsByVehicleId(int id) => _bplManager.GetMembershipDetailsByVehicleId(id);
        [HttpGet]
        [Route("GetPastDetails/{clientId}")]
        public Result GetPastDetails(int clientId) => _bplManager.GetPastDetails(clientId);

    }
}