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
    public class TenantBpl : Strivebase, ITenantBpl
    {
        public TenantBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result CreateTenant(TenantCreateViewModel tenant)
        {
            try
            {
                var common = new CommonBpl(_cache, _tenant);
                var newPassword = common.RandomString(6);
                var hashPassword = Pass.Hash(newPassword);
                tenant.TenantViewModel.PasswordHash = hashPassword;
                var saveStatus = new TenantRal(_tenant, true).CreateTenant(tenant.TenantViewModel);

                //Add Module
                var tenantModule = new TenantRal(_tenant, false).AddModule(tenant.TenantModuleViewModel);

                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                keyValues.Add("{{emailId}}", tenant.TenantViewModel.TenantEmail);
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
            return ResultWrap(new TenantRal(_tenant, true).GetAllTenant, "AllTenant");
        }
        public TenantModulesViewModel GetTenantById(int id)
        {
            var result = new TenantModulesViewModel();
            result.TenantViewModel = new TenantRal(_tenant, true).GetTenantById(id);
            result.TenantModuleViewModel = new TenantRal(_tenant, false).GetModuleById(result.TenantViewModel.TenantId);
            return result;

        }
        public Result GetAllModule()
        {
            return ResultWrap(new TenantRal(_tenant, true).GetAllModule, "AllModule");
        }
        public Result UpdateTenant(TenantCreateViewModel tenant)
        {
            //Edit Module
            var tenantModule = new TenantRal(_tenant, false).UpdateModule(tenant.TenantModuleViewModel);

            return ResultWrap(new TenantRal(_tenant, true).UpdateTenant(tenant.TenantViewModel), "UpdateTenant");
        }

    }
}
