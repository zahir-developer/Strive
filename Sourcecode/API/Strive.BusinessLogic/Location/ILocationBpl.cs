using System.Collections.Generic;
using Strive.Common;

namespace Strive.BusinessLogic.Location
{
    public interface ILocationBpl
    {
        Result GetLocationDetails();
        Result SaveLocationDetails(List<BusinessEntities.Location> lstLocation);
        Result DeleteLocationDetails(int id);
        Result GetLocationById(int id);
    }
}
