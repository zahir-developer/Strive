using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.MembershipSetup;
using Strive.BusinessEntities.MembershipSetup;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.MembershipSetup
{
    public class MembershipBpl : Strivebase, IMembershipBpl
    {
        public MembershipBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result GetAllMembership()
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetAllMembership, "Membership");
        }
       
        public Result GetMembershipById(int membershipid)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetMembershipById,membershipid, "MembershipDetail");
        }

        public Result GetMembershipAvailability(int vehicleId)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetMembershipAvailability, vehicleId, "IsMemebership");
        }
        public Result AddMembership(MembershipDto member)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).AddMembership, member, "Status");
        }

        public Result UpdateMembership(MembershipDto member)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).UpdateMembership, member, "Status");
        }
        public Result DeleteMembershipById(int membershipid)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).DeleteMembershipById, membershipid, "Status");
        }
        public Result GetMembershipAndServiceByMembershipId(int id)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetMembershipAndServiceByMembershipId, id, "MembershipAndServiceDetail");
        }
        public Result GetMembershipSearch(MembershipSearchDto search)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetMembershipSearch, search, "MembershipSearch");
        }

        public Result GetVehicleMembershipByMembershipId(int membershipId)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetVehicleMembershipByMembershipId, membershipId, "VehicleMembershipByMembershipId");
        }
        public Result GetAllMembershipName()
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetAllMembershipName, "MembershipName");
        }
        public Result GetMembershipDetails(MembershipSearch search)
        {
            return ResultWrap(new MembershipSetupRal(_tenant).GetMembershipDetails,search, "MembershipValidation");
        }

    }
}
