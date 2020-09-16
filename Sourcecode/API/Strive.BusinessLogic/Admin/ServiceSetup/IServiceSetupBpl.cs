using Strive.BusinessEntities.DTO.ServiceSetup;
using Strive.BusinessEntities.Model;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic
{
    public interface IServiceSetupBpl
    {
        Result AddService(Service service);
        Result UpdateService(Service service);
        Result GetSearchResult(ServiceSearchDto search);
        Result DeleteServiceById(int id);
        Result GetAllServiceSetup();
        Result GetAllServiceType();
        Result GetServicesDetailsWithUpcharges();
        Result GetServiceSetupById(int id);
        Result GetServiceCategoryByLocationId(int id);
        
    }
}
