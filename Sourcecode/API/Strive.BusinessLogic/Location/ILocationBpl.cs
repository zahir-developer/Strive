using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic
{
    public interface ILocationBpl
    {
        Result GetLocationDetails();
        Result SaveLocationDetails(List<Location> lstLocation);
        Result DeleteLocationDetails(int id);
        Result GetLocationById(int id);
    }
}
