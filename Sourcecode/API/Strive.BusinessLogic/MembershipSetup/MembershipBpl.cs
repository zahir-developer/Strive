using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
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
        public MembershipBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper) { }

        public Result GetAllMembership()
        {
            try
            {
                var lstMembership = new MembershipSetupRal(_tenant).GetAllMembership();
                _resultContent.Add(lstMembership.WithName("MembershipSetup"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetServicesWithPrice()
        {
            try
            {
                var lstServiceWithPrice = new MembershipSetupRal(_tenant).GetServicesWithPrice();
                _resultContent.Add(lstServiceWithPrice.WithName("GetServiceWithPrice"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetMembershipById(int membershipid)
        {
            try
            {
                var lstMembershipById = new MembershipSetupRal(_tenant).GetMembershipById(membershipid);
                _resultContent.Add(lstMembershipById.WithName("MembershipDetail"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveMembershipSetup(List<MembershipView> member)
        {
            try
            {
                bool blnStatus = new MembershipSetupRal(_tenant).SaveMembershipSetup(member);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;

        }
        public Result DeleteMembershipById(int membershipid)
        {
            try
            {
                var lstMembership = new MembershipSetupRal(_tenant).DeleteMembershipById(membershipid);
                _resultContent.Add(lstMembership.WithName("Membership"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

    }
}

