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

namespace Strive.ResourceAccess
{
    public class SuperAdminRal : RalBase
    {
        public SuperAdminRal(ITenantHelper tenant, bool isAuth = false) : base(tenant, isAuth)
        {
        }

        public bool CreateTenant(TenantViewModel tenant)
        {
            _prm.Add("@FirstName", tenant.FirstName);
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

            CommandDefinition cmd = new CommandDefinition(SPEnum.USPCREATETENANT.ToString(), _prm, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool AddModule(TenantListModuleViewModel module)
        {
            return dbRepo.InsertPc(module, "ModuleId");
        }
        public List<ClientTenantViewModel> GetAllTenant()
        {           
            return db.Fetch<ClientTenantViewModel>(SPEnum.uspGetTenant.ToString(), _prm);
        }
        public List<ClientTenantViewModel> GetTenantById(int id)
        {

            _prm.Add("ClientId", id);
            return db.Fetch<ClientTenantViewModel>(SPEnum.uspGetTenantById.ToString(), _prm);
        }
        public List<TenantModuleViewModel> GetAllModule()
        {
            return db.Fetch<TenantModuleViewModel>(SPEnum.uspGetAllModule.ToString(), _prm);
        }
    }
}
