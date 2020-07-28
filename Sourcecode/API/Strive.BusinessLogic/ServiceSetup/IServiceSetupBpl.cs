using Strive.BusinessEntities.ServiceSetup;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic
{
    public interface IServiceSetupBpl
    {
        Result GetServiceSetupDetails();
        Result SaveNewServiceDetails(List<Service> lstServiceSetup);
        Result DeleteServiceById(int id);
        Result GetServiceSetupById(int id);
    }
}
