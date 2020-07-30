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

namespace Admin.Api.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class MembershipSetupController : ControllerBase
    {
        IMembershipBpl _membershipBpl = null;

        public MembershipSetupController(IMembershipBpl membershipBpl)
        {
            _membershipBpl = membershipBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllMembership()
        {
            return _membershipBpl.GetAllMembership();
        }
        [HttpGet]
        [Route("GetService")]
        public Result GetServiceWithPrice()
        {
            return _membershipBpl.GetServicesWithPrice();
        }
        [HttpGet]
        [Route("GetAllMembershipById/{membershipId}")]
        public Result GetMembershipById(int membershipId)
        {
            return _membershipBpl.GetMembershipById(membershipId);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveMembershipSetup([FromBody] List<MembershipView> member)
        {
            return _membershipBpl.SaveMembershipSetup(member);
        }

        [HttpDelete]
        [Route("{membershipId}")]
        public Result DeleteMembershipById(int membershipId)
        {
            return _membershipBpl.DeleteMembershipById(membershipId);
        }

    }
}