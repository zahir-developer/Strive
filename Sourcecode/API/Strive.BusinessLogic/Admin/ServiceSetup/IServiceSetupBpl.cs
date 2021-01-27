﻿using Strive.BusinessEntities.DTO;
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
        Result GetAllServiceType();
        Result GetAllServiceSetup(SearchDto searchDto);
        Result GetServiceSetupById(int id);
        Result DeleteServiceById(int id);
        Result GetServiceSearch(ServiceSearchDto search);
        Result GetServiceCategoryByLocationId(int id);
        Result GetServicesWithPrice();
    }
}
