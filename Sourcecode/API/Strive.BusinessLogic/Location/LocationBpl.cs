using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        ITenantHelper tenant;
        JObject result = new JObject();

        LocationRal LocationRal;
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
           
        }

        public Result GetAllLocation()
        {
            try
            {
                var list = new LocationRal(tenant).GetAllLocation();

                result.Add(list.WithName("Location"));

                return Helper.BindSuccessResult(result);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Result SaveLocation(List<Location> location)
        {
            try
            {
                var saveresult = LocationRal.SaveLocation(location);

                result.Add(saveresult.WithName("Status"));

                return Helper.BindSuccessResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result AddLocation(List<Location> locations)
        {
            try
            {
                var success = LocationRal.SaveLocation(locations);

                result.Add(success.WithName("Status"));

                return Helper.BindSuccessResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
