using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ServiceSetup;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic
{
    public interface IServiceSetupBpl
    {
        Result AddService(Service service);
        Result UpdateService(Service service);
        Result GetAllServiceType();
        Result GetAllServiceSetup();
        Result GetServiceSetupById(int id);
        Result DeleteServiceById(int id);
    }
}
