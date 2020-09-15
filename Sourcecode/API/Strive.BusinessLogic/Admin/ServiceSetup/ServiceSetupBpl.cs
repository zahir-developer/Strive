using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.ServiceSetup;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.ResourceAccess;

namespace Strive.BusinessLogic.ServiceSetup
{
    public class ServiceSetupBpl : Strivebase, IServiceSetupBpl
    {
        public ServiceSetupBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }


        public Result AddService(Service service)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).AddService, service, "Status");
        }

        public Result UpdateService(Service service)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).UpdateService, service, "Status");
        }

        public Result GetAllServiceType()
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetAllServiceType, "ServiceType");
        }

        public Result GetAllServiceSetup()
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetAllServiceSetup, "ServiceSetup");
        }

        public Result GetServiceSetupById(int id)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetServiceSetupById, id, "ServiceSetup");
        }

        public Result DeleteServiceById(int id)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).DeleteServiceById, id, "ServiceDelete");
        }
        public Result GetServiceSearch(ServiceSearchDto search)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetServiceSearch, search, "ServiceSearch");
        }
        public Result GetServiceCategoryByLocationId(int id)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetServiceCategoryByLocationId, id, "ServiceCategory");
        }
        public Result GetServicesWithPrice()
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetServicesWithPrice, "ServicesWithPrice");
        }
    }
}