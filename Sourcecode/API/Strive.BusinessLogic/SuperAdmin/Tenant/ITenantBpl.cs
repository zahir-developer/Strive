using Strive.BusinessEntities;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
    public interface ITenantBpl
    {
        Result CreateTenant(TenantCreateViewModel tenant, string connection);
        Result GetAllTenant();
        TenantModulesViewModel GetTenantById(int id);
        Result GetAllModule();
        Result UpdateTenant(TenantCreateViewModel tenant);
        Result GetState();
        Result GetCityByStateId(int id);
    }
}
