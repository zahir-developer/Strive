using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Location;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using GoogleMaps.LocationServices;
using Strive.BusinessEntities.DTO.MembershipSetup;

namespace Strive.BusinessLogic.Location
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        private List<Bay> CreateBay()
        {
            List<Bay> bay = new List<Bay>();
            bay.Add(new Bay() { BayName = "Detail Bay 1", IsActive = true, IsDeleted = false, CreatedBy = _tenant.EmployeeId.toInt(), CreatedDate = DateTime.UtcNow });
            bay.Add(new Bay() { BayName = "Detail Bay 2", IsActive = true, IsDeleted = false, CreatedBy = _tenant.EmployeeId.toInt(), CreatedDate = DateTime.UtcNow });
            bay.Add(new Bay() { BayName = "Detail Bay 3", IsActive = true, IsDeleted = false, CreatedBy = _tenant.EmployeeId.toInt(), CreatedDate = DateTime.UtcNow });
            return bay;
        }
        private Drawer CreateDrawer()
        {
            return new Drawer() { DrawerName = "Drawer1", IsActive = true, IsDeleted = false, CreatedBy = _tenant.EmployeeId.toInt(), CreatedDate = DateTime.UtcNow };
        }
        public Result AddLocation(LocationDto location)
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));

            location.Location.ColorCode = color;
            location.Location.WashTimeMinutes = random.Next(30, 45);
            location.Bay = CreateBay();
            location.Drawer = CreateDrawer();
            CommonBpl commonBpl = new CommonBpl(_cache, _tenant);
            List<Geocode> lstGeocode = commonBpl.GetGeocode(location.LocationAddress);
            if (lstGeocode.Count > 0)
            {
                location.LocationAddress.Latitude = Convert.ToDecimal(lstGeocode[0].lat);
                location.LocationAddress.Longitude = Convert.ToDecimal(lstGeocode[0].lon);
            }
            try
            {
                var LocationGeo = GetLocationGeo(location.LocationAddress);

            }
            catch (Exception ex)
            {

            }
            //var apiLocationId = CreateLocationForWeatherPortal();

            //location.Drawer = new BusinessEntities.Model.Drawer();
            //bool status = new LocationRal(_tenant).AddLocation(location);     
            
            var locationId =new LocationRal(_tenant).AddLocation(location);

            var result =Bayslot(locationId);

            return ResultWrap(locationId, "Status");

        }

        public Result UpdateLocation(LocationDto location)
        {
            CommonBpl commonBpl = new CommonBpl(_cache, _tenant);
            List<Geocode> lstGeocode = commonBpl.GetGeocode(location.LocationAddress);
            if (lstGeocode.Count > 0)
            {
                location.LocationAddress.Latitude = Convert.ToDecimal(lstGeocode[0].lat);
                location.LocationAddress.Longitude = Convert.ToDecimal(lstGeocode[0].lon);
            }
            return ResultWrap(new LocationRal(_tenant).UpdateLocation, location, "Status");
        }

        public Result GetLocationSearch(LocationSearchDto search)
        {
            return ResultWrap(new LocationRal(_tenant).GetLocationSearch, search, "Search");
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
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(locationAddress.Address1);
            locationAddress.Latitude = point.Latitude.toDecimal();
            var longitude = point.Longitude;
            return "s";

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


        public Result AddLocationOffset(LocationOffsetDto locationOffset)
        {
            try
            {
                return ResultWrap(new LocationRal(_tenant).AddLocationOffset, locationOffset, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result UpdateLocationOffset(LocationOffsetDto locationOffset)
        {
            try
            {
                return ResultWrap(new LocationRal(_tenant).UpdateLocationOffset, locationOffset, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetAllLocationOffset()
        {
            return ResultWrap(new LocationRal(_tenant).GetAllLocationOffset, "Location");
        }
        public Result DeleteLocationOffset(int id)
        {
            return ResultWrap(new LocationRal(_tenant).DeleteLocationOffset, id, "LocationDelete");
        }
        public Result Bayslot (int id)
        {

            return ResultWrap(new LocationRal(_tenant).AddBaySolt, id, "bayslot");
        }


        public Result GetAllLocationName()
        {
            return ResultWrap(new LocationRal(_tenant).GetAllLocationName, "Location");
        }
        public Result GetMerchantDetails(MerchantSearch search)
        {
            return ResultWrap(new LocationRal(_tenant).GetMerchantDetails, search, "MerchantValidation");
        }
    }
}

