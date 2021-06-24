using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using Strive.BusinessEntities.Employee;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Strive.BusinessLogic.MembershipSetup;
using Strive.BusinessEntities.MembershipSetup;
using Admin.API.Helpers;
using Strive.BusinessEntities.DTO.MembershipSetup;

namespace Admin.Api.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class MembershipSetupController : StriveControllerBase<IMembershipBpl>
    {
        public MembershipSetupController(IMembershipBpl mrsBpl) : base(mrsBpl) { }
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllMembership() => _bplManager.GetAllMembership();
        [HttpGet]
        [Route("GetAllMembershipById/{membershipId}")]
        public Result GetMembershipById(int membershipId) => _bplManager.GetMembershipById(membershipId);
        [HttpGet]
        [Route("GetMembershipAvailability/{vehicleId}")]
        public Result GetMembershipAvailability(int vehicleId) => _bplManager.GetMembershipAvailability(vehicleId);
        [HttpPost]
        [Route("Add")]
        public Result AddMembership([FromBody] MembershipDto member) => _bplManager.AddMembership(member);
        [HttpPost]
        [Route("Update")]
        public Result UpdateMembership([FromBody] MembershipDto member) => _bplManager.UpdateMembership(member);
        [HttpDelete]
        [Route("Delete/{membershipId}")]
        public Result DeleteMembershipById(int membershipId) => _bplManager.DeleteMembershipById(membershipId);
        [HttpGet]
        [Route("GetMembershipAndServiceByMembershipId/{id}")]
        public Result GetMembershipAndServiceByMembershipId(int id) => _bplManager.GetMembershipAndServiceByMembershipId(id);

        [HttpPost]
        [Route("GetMembershipSearch")]
        public Result GetMembershipSearch([FromBody] MembershipSearchDto search) => _bplManager.GetMembershipSearch(search);


        [HttpGet]
        [Route("GetVehicleMembershipByMembershipId/{membershipId}")]
        public Result GetVehicleMembershipByMembershipId(int membershipId) => _bplManager.GetVehicleMembershipByMembershipId(membershipId);

        [HttpGet]
        [Route("GetAllMembershipName")]
        public Result GetAllMembershipName() => _bplManager.GetAllMembershipName();


    }
}