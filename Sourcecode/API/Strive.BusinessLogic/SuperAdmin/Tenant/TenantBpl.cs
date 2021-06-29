using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
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

        public Result CreateTenant(TenantCreateViewModel tenant, string connection)
        {
            try
            {
                bool saveStatus = false;
                if (tenant != null)
                {
                    var common = new CommonBpl(_cache, _tenant);
                    string newPassword = common.RandomString(6);
                    string hashPassword = Pass.Hash(newPassword);
                    tenant.TenantViewModel.PasswordHash = hashPassword;

                    string tenantGuid = new TenantRal(_tenant, true).CreateTenant(tenant.TenantViewModel);

                    //Change Tenant Connection
                    /*
                    Guid guid = new Guid(tenantGuid);
                    TenantSchema tSchema = new TenantRal(_tenant, true).TenantAdminLogin(guid);
                    CacheLogin(tSchema, connection);
                    */

                    // Add Module
                    //foreach (var item in tenant.Module)
                    //{
                    //    var tenantModule = new TenantRal(_tenant, false).AddModule(item);
                    //}



                    //Send email
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("{{emailId}}", tenant.TenantViewModel.TenantEmail);
                    keyValues.Add("{{password}}", newPassword);
                    common.SendLoginCreationEmail(HtmlTemplate.SuperAdmin, tenant.TenantViewModel.TenantEmail, newPassword);
                    saveStatus = true;
                }

                _resultContent.Add(saveStatus.WithName("SaveStatus"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetAllTenant(SearchDto searchDto)
        {
            return ResultWrap(new TenantRal(_tenant, true).GetAllTenant,searchDto, "AllTenant");
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
            try
            {
                //Edit Module
                foreach (var item in tenant.Module)
                {
                    var tenantModule = new TenantRal(_tenant, false).UpdateModule(item);
                }


                //var tenantModule = new TenantRal(_tenant, false).UpdateModule(tenant.TenantModuleViewModel.Module);

                return ResultWrap(new TenantRal(_tenant, true).UpdateTenant(tenant.TenantViewModel), "UpdateTenant");

            }
            catch (Exception ex)
            {
                return Helper.BindFailedResult(ex, HttpStatusCode.InternalServerError);
            }
        }
            
        private void CacheLogin(TenantSchema tSchema, string tcon)
        {
            SetTenantSchematoCache(tSchema);
            _tenant.SetConnection(GetTenantConnectionString(tSchema, tcon));

        }
        public Result GetState()
        {
            return ResultWrap(new TenantRal(_tenant, true).GetState, "Allstate");
        }
        public Result GetCityByStateId(int stateId)
        {
            return ResultWrap(new TenantRal(_tenant, true).GetCityByStateId, stateId, "cities");

        }
        public Result GetLocationMaxLimit(int tenantId)
        {

            return ResultWrap(new TenantRal(_tenant, true).GetLoationMaxLimit, tenantId, "maxCount");


        }
    }
}
