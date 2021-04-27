using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.Crypto;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
    public class SuperAdminBpl : Strivebase, ISuperAdminBpl
    {
        public SuperAdminBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result CreateTenant(TenantViewModel tenant)
        {
            try
            {
                var common = new CommonBpl(_cache, _tenant);
                var newPassword = common.RandomString(6);
                var hashPassword = Pass.Hash(newPassword);
                tenant.PasswordHash = hashPassword;
                var saveStatus = new SuperAdminRal(_tenant, true).CreateTenant(tenant);

                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("{{emailId}}", tenant.TenantEmail);
                keyValues.Add("{{password}}", newPassword);
                

                //common.SendLoginCreationEmail(HtmlTemplate.SuperAdmin, tenant.TenantEmail, newPassword);

                _resultContent.Add(saveStatus.WithName("SaveStatus"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetAllTenant()
        {
            return ResultWrap(new SuperAdminRal(_tenant, true).GetAllTenant,"AllClients");
        }
        public Result GetTenantById(int id)
        {
            return ResultWrap(new SuperAdminRal(_tenant, true).GetTenantById,id,"TenantById");
        }

    }
}
