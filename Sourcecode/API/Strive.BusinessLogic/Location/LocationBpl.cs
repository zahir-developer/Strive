using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.Location;
using Strive.Common;
using Strive.ResourceAccess;
using System.Linq;
using Strive.BusinessLogic.Common;

namespace Strive.BusinessLogic.Location
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result GetLocationDetails()
        {
            try
            {
                var lstLocation = new LocationRal(_tenant).GetLocationDetails();
                _resultContent.Add(lstLocation.WithName("Location"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveLocationDetails(LocationDto location)
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
        public Result DeleteLocationDetails(int id)
        {
            try
            {
                var lstLocation = new LocationRal(_tenant).DeleteLocationDetails(id);
                _resultContent.Add(lstLocation.WithName("Location"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GetLocationById(int id)
        {
            try
            {
                var lstLocation = new LocationRal(_tenant).GetLocationById(id);
                _resultContent.Add(lstLocation.WithName("Location"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result AddLocation(LocationDto location)
        {
            try
            {
                bool blnStatus = new LocationRal(_tenant).AddLocation(location);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result UpdateLocation(LocationDto location)
        {
            try
            {
                bool blnStatus = new LocationRal(_tenant).UpdateLocation(location);
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
