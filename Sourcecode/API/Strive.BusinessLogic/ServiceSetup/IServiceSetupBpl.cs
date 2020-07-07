using Strive.BusinessEntities.ServiceSetup;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic
{
    public interface IServiceSetupBpl
    {
        Result GetServiceSetupDetails();
        Result GetAllServiceType();
        Result SaveNewServiceDetails(List<tblService> lstServiceSetup);
        Result DeleteServiceById(int id);
        Result GetServiceSetupById(int id);
    }
}
