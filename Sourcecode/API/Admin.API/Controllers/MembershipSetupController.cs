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
        //[HttpGet]
        //[Route("GetAll")]
        //public Result GetAllMembership()
        //{
        //    return _membershipBpl.GetAllMembership();
        //}
        [HttpGet]
        [Route("GetService")]
        public Result GetServiceWithPrice() => _bplManager.GetServicesWithPrice();
        //[HttpGet]
        //[Route("GetService")]
        //public Result GetServiceWithPrice()
        //{
        //    return _membershipBpl.GetServicesWithPrice();
        //}
        [HttpGet]
        [Route("GetAllMembershipById/{membershipId}")]
        public Result GetMembershipById(int membershipId) => _bplManager.GetMembershipById(membershipId);
        [HttpPost]
        [Route("Add")]
        public Result AddMembership([FromBody] List<MembershipDto> member) => _bplManager.AddMembership(member);


        [HttpPost]
        [Route("Update")]
        public Result UpdateMembership([FromBody] List<MembershipDto> member) => _bplManager.UpdateMembership(member);
        [HttpPost]
        //[Route("Save")]
        //public Result SaveMembershipSetup([FromBody] List<MembershipView> member)
        //{
        //    return _membershipBpl.SaveMembershipSetup(member);
        //}
        [HttpDelete]
        [Route("Delete/{membershipId}")]
        public Result DeleteMembershipById(int membershipId) => _bplManager.DeleteMembershipById(membershipId);
        //[HttpDelete]
        //[Route("{membershipId}")]
        //public Result DeleteMembershipById(int membershipId)
        //{
        //    return _membershipBpl.DeleteMembershipById(membershipId);
        //}

    }
}