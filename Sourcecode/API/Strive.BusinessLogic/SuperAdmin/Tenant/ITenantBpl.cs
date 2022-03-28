using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;

namespace Strive.BusinessLogic.SuperAdmin.Tenant
{
    public interface ITenantBpl
    {
        Result CreateTenant(TenantCreateViewModel tenant, string connection);
        Result GetAllTenant(SearchDto searchDto);
        TenantModulesViewModel GetTenantById(int id);
        Result GetAllModule();
        Result UpdateTenant(TenantCreateViewModel tenant);
        Result GetState();
        Result GetCityByStateId(int id);
        Result GetLocationMaxLimit();
    }
}
