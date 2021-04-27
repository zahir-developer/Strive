using Strive.BusinessEntities.ViewModel;
using Strive.Common;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
     public interface ISuperAdminBpl
    {
        Result CreateTenant(TenantViewModel tenant);
        Result GetAllTenant();
        Result GetTenantById(int id);
        Result GetAllModule();
    }
}
