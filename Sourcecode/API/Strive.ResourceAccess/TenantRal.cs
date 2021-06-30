using Dapper;
using Strive.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Strive.Repository;
using System.Linq;
using System.Data;
using Strive.Common;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.City;
using Strive.BusinessEntities.DTO;

namespace Strive.ResourceAccess
{
    public class TenantRal : RalBase
    {
        public TenantRal(ITenantHelper tenant, bool isAuth = false, bool isTenantAdmin = false) : base(tenant, isAuth, isTenantAdmin)
        {
        }

        public TenantSchema TenantAdminLogin(Guid tenantGuid)
        {
            try
            {
                var dynParams = new DynamicParameters();
                dynParams.Add("@TenantGuid", tenantGuid);
                var res = db.Fetch<TenantSchema>(EnumSP.Tenant.USPTENANTADMINLOGIN.ToString(), dynParams);
                if (res.Count() == 0) throw new Exception("data returned null value");
                return res?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateTenant(TenantViewModel tenant)
        {
            _prm.Add("@FirstName", tenant.FirstName);
            _prm.Add("@LastName", tenant.LastName);
            _prm.Add("@Address", tenant.Address);
            _prm.Add("@MobileNumber", tenant.MobileNumber);
            _prm.Add("@PhoneNumber", tenant.PhoneNumber);
            _prm.Add("@TenantName", tenant.TenantName);
            _prm.Add("@TenantEmail", tenant.TenantEmail);
            _prm.Add("@Subscriptionid", tenant.SubscriptionId);
            _prm.Add("@SubscriptionDate", tenant.SubscriptionDate);
            _prm.Add("@Locations", tenant.Locations);
            _prm.Add("@PaymentDate", tenant.PaymentDate);
            _prm.Add("@SchemaPasswordHash", tenant.PasswordHash);
            _prm.Add("@ExpiryDate", tenant.ExpiryDate);
            _prm.Add("ZipCode", tenant.ZipCode);
            _prm.Add("@State", tenant.State);
            _prm.Add("@IsActive", tenant.IsActive);
            _prm.Add("@City", tenant.City);
            var result = (string)db.Get<string>(EnumSP.Tenant.USPCREATETENANT.ToString(), _prm);

            return result;
        }
        public int AddModule(TenantListModuleViewModel module)
        {
            return dbRepo.InsertPK(module, "ModuleId");
        }
        public bool UpdateTenant(TenantViewModel tenant)
        {
            _prm.Add("@FirstName", tenant.FirstName);
            _prm.Add("@LastName", tenant.LastName);
            _prm.Add("@Address", tenant.Address);
            _prm.Add("@MobileNumber", tenant.MobileNumber);
            _prm.Add("@PhoneNumber", tenant.PhoneNumber);
            _prm.Add("@TenantName", tenant.TenantName);
            _prm.Add("@SubscriptionDate", tenant.SubscriptionDate);
            _prm.Add("@Locations", tenant.Locations);
            _prm.Add("@PaymentDate", tenant.PaymentDate);
            _prm.Add("@ExpiryDate", tenant.ExpiryDate);
            _prm.Add("@ClientId", tenant.ClientId);
            _prm.Add("@TenantId", tenant.TenantId);
            _prm.Add("ZipCode", tenant.ZipCode);
            _prm.Add("@State", tenant.State);
            _prm.Add("@City", tenant.City);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Tenant.USPUPDATETENANT.ToString(), _prm, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool UpdateModule(TenantListModuleViewModel module)
        {
            return dbRepo.UpdatePc(module);
        }
        public ClientTenantGridViewModel GetAllTenant(SearchDto searchDto)
        {
            _prm.Add("PageNo", searchDto.PageNo);
            _prm.Add("PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            return db.FetchMultiResult<ClientTenantGridViewModel>(EnumSP.Tenant.USPGETTENANT.ToString(), _prm);
        }
        public ClientTenantViewModel GetTenantById(int id)
        {
            _prm.Add("ClientId", id);
            return db.FetchSingle<ClientTenantViewModel>(EnumSP.Tenant.USPGETTENANTBYID.ToString(), _prm);
        }
        public ModuleListDto GetAllModule()
        {
            return db.FetchMultiResult<ModuleListDto>(EnumSP.Tenant.USPGETALLMODULE.ToString(), _prm);
        }
        public ModuleListDto GetModuleById(int id)
        {
            _prm.Add("@TenantId", id);
            return db.FetchMultiResult<ModuleListDto>(EnumSP.Tenant.USPGETMODULEBYID.ToString(), _prm);
        }
        public List<StateViewModel> GetState()
        {
            return db.Fetch<StateViewModel>(EnumSP.Tenant.USPGETSTATE.ToString(), _prm);
        }
        public List<CityDto> GetCityByStateId(int stateId)
        {
            _prm.Add("stateId", stateId);
            return db.Fetch<CityDto>(EnumSP.Tenant.USPGETCITYBYSTATEID.ToString(), _prm);
        }

        public MaxLocationViewModel GetLoationMaxLimit(int tenantId)
        {
            _prm.Add("tenantGuidId", tenantId);
            return db.FetchSingle<MaxLocationViewModel>(EnumSP.Tenant.USPGETLOCATIONLIMIT.ToString(), _prm);
        }
    }
}
