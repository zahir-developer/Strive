using Strive.BusinessEntities.ViewModel;
using Strive.Common;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
     public interface ITenantBpl
    {
        Result CreateTenant(TenantCreateViewModel tenant);
        Result GetAllTenant();
        TenantModulesViewModel GetTenantById(int id);
        Result GetAllModule();
        Result UpdateTenant(TenantCreateViewModel tenant);
    }
}
