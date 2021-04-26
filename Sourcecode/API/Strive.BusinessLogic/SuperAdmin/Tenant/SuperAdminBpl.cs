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

                var subject = "Login has been created for " + tenant.TenantName;

                var bodyMsg = @",</p>
            <p>Login has been created. Login use 'UserName: " + tenant.TenantEmail + "'Password:" + newPassword + @"'.</p>
            <p>Thanks,</p>
            <p>Strive Team.</p>";

                //common.SendMail(tenant.TenantEmail, bodyMsg, subject);

                _resultContent.Add(saveStatus.WithName("SaveStatus"));
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
