using Strive.BusinessEntities.ViewModel;
using Strive.Common;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
     public interface ISuperAdminBpl
    {
        Result CreateTenant(TenantCreateViewModel tenant);
        Result GetAllTenant();
        Result GetTenantById(int id);
        Result GetAllModule();
        Result UpdateTenant(TenantCreateViewModel tenant);
    }
}
