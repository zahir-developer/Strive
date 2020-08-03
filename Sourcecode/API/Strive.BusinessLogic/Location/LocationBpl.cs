using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Net;

namespace Strive.BusinessLogic.Location
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result AddLocation(LocationDto location)
        {
            location.Drawer = new BusinessEntities.Model.Drawer();
            return ResultWrap(new LocationRal(_tenant).AddLocation, location, "Status");
        }

        public Result UpdateLocation(LocationDto location)
        {
            return ResultWrap(new LocationRal(_tenant).UpdateLocation, location, "Status");
        }

        public Result GetAllLocation()
        {
            return ResultWrap(new LocationRal(_tenant).GetAllLocation, "Location");
        }

        public Result GetLocationById(int id)
        {
            return ResultWrap(new LocationRal(_tenant).GetLocationDetailById, id, "Location");
        }

        public Result DeleteLocation(int id)
        {
            return ResultWrap(new LocationRal(_tenant).DeleteLocation, id, "LocationDelete");
        }

        public Result SaveLocation(LocationDto location)
        {
            try
            {
                CommonBpl commonBpl = new CommonBpl(_cache, _tenant);
                var lstGeocode = commonBpl.GetGeocode(location.LocationAddress);

                bool blnStatus = new LocationRal(_tenant).SaveLocationDetails(location);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

    }
}
