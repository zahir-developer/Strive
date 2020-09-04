using Strive.BusinessEntities.ViewModel;
using Strive.Common;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
    interface ISuperAdminBpl
    {
        Result CreateTenant(TenantViewModel tenant);
    }
}
