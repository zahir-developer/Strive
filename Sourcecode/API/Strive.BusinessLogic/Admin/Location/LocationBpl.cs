using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.Model;
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
            ////CommonBpl commonBpl = new CommonBpl(_cache, _tenant);
            ////var lstGeocode = commonBpl.GetGeocode(location.LocationAddress);

            //var LocationGeo = GetLocationGeo(location.LocationAddress);
            //var apiLocationId = CreateLocationForWeatherPortal();

            //location.Drawer = new BusinessEntities.Model.Drawer();
            //bool status = new LocationRal(_tenant).AddLocation(location);
            return ResultWrap(new LocationRal(_tenant).AddLocation, location, "Status");

        }

        public Result UpdateLocation(LocationDto location)
        {
           // CommonBpl commonBpl = new CommonBpl(_cache, _tenant);
           // var lstGeocode = commonBpl.GetGeocode(location.LocationAddress);
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


        private string GetLocationGeo(LocationAddress locationAddress)
        {
            throw new NotImplementedException();
        }

        //public async Task<Result> CreateLocationForWeatherPortal()
        //{
        //    const string baseUrl = "https://api.climacell.co/";
        //    const string apiMethod = "v3/locations";
        //    //const string apiKey = "sbXIC0D1snD0d4SrQEXPdG8iNiD1mOLV";
        //    const string apiKey = "YdGO72oCIGiaTxqfGEOhD9fty8fHAVdr";



        //    var weatherlocation = new WeatherLocation()
        //    {
        //        name = "Strive-Location1",
        //        point = new point() { lat = 34.07, lon = -84.29 }
        //    };
        //    var wlocation = JsonConvert.SerializeObject(weatherlocation);
        //    var stringContent = new StringContent(wlocation, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+




            



        //    return null;
        //}

    }
}
