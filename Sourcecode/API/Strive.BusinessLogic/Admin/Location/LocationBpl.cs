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

namespace Strive.BusinessLogic.Location
{
    public class LocationBpl : Strivebase, ILocationBpl
    {
        public LocationBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        
        public Result AddLocation(LocationDto location)
        {
            string GetRandomColor = new CommonBpl(_cache, _tenant).RandomColorGenerator();
            location.Location.ColorCode = GetRandomColor;
            ///to-do: WashTimeMinutes is hardcorded as of Now, Once got replied from Client it will be removed
            location.Location.WashTimeMinutes = 30;
            Drawer(location);
            Bay(location);
            return ResultWrap(new LocationRal(_tenant).AddLocation, location, "Status");
        }
        private string Drawer(LocationDto location)
        {
            location.Drawer = new Drawer();
            return location.Drawer.DrawerName = "Drawer 1";
        }
        private List<Bay> Bay(LocationDto location)
        {
            List<Bay> bay = new List<Bay>();
            bay.Add(new Bay() { BayName = "Bay 1", IsActive = true, IsDeleted = false });
            bay.Add(new Bay() { BayName = "Bay 2", IsActive = true, IsDeleted = false });
            bay.Add(new Bay() { BayName = "Bay 3", IsActive = true, IsDeleted = false });
            return location.Bay = bay;
        }
        public Result UpdateLocation(LocationDto location)
        {
            return ResultWrap(new LocationRal(_tenant).UpdateLocation, location, "Status");
        }

        public Result DeleteLocation(int id)
        {
            return ResultWrap(new LocationRal(_tenant).DeleteLocation, id, "LocationDelete");
        }

        public Result GetSearchResult(LocationSearchDto search)
        {
            return ResultWrap(new LocationRal(_tenant).GetSearchResult, search,"Search");
        }

        public Result GetAllLocation()
        {
            return ResultWrap(new LocationRal(_tenant).GetAllLocation, "Location");
        }

        public Result GetLocationById(int id)
        {
            return ResultWrap(new LocationRal(_tenant).GetLocationDetailById, id, "Location");
        }
    }
}
