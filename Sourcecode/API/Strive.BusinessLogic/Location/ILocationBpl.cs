using System.Collections.Generic;
using Strive.BusinessEntities.Location;
using Strive.Common;

namespace Strive.BusinessLogic.Location
{
    public interface ILocationBpl
    {
        Result GetLocationDetails();
        Result SaveLocationDetails(LocationView lstLocation);
        Result DeleteLocationDetails(int id);
        Result GetLocationById(int id);
        Result AddLocation(List<LocationView> lstLocation);
        Result UpdateLocation(List<LocationView> lstLocation);

    }
}
