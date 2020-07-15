using System.Collections.Generic;
using Strive.Common;

namespace Strive.BusinessLogic.Location
{
    public interface ILocationBpl
    {
        Result GetLocationDetails();
        Result SaveLocationDetails(List<BusinessEntities.LocationList> lstLocation);
        Result DeleteLocationDetails(int id);
        Result GetLocationById(int id);
        Result AddLocation(List<BusinessEntities.LocationList> lstLocation);
        Result UpdateLocation(List<BusinessEntities.LocationList> lstLocation);

    }
}
