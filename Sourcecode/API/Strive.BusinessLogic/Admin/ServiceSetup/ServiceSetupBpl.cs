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

        public Result GetSearchResult(ServiceSearchDto search)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetSearchResult, search, "ServiceSearch");
        }

        public Result DeleteServiceById(int id)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).DeleteServiceById, id, "ServiceDelete");
        }

        public Result GetAllServiceSetup()
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetAllServiceSetup, "ServiceSetup");
        }

        public Result GetAllServiceType()
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetAllServiceType, "ServiceType");
        }

        public Result GetServicesDetailsWithUpcharges()
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetServicesDetailsWithUpcharges, "ServicesWithPrice");
        }

        public Result GetServiceSetupById(int id)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetServiceSetupById, id, "ServiceSetup");
        }

        public Result GetServiceCategoryByLocationId(int id)
        {
            return ResultWrap(new ServiceSetupRal(_tenant).GetServiceCategoryByLocationId, id, "ServiceCategory");
        }
    }
}