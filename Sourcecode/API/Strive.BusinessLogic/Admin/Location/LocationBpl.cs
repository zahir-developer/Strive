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
            location.Location.ColorCode = Utility.GetRandomColor();

            ///to-do: WashTimeMinutes is hardcorded as of now. washtime logic yet to be confirmed.
            location.Location.WashTimeMinutes = 30;

            location.Drawer = CreateDrawer();
            location.Bay = CreateBay();

            return ResultWrap(new LocationRal(_tenant).AddLocation, location, "Status");
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
            return ResultWrap(new LocationRal(_tenant).GetSearchResult, search, "Search");
        }

        public Result GetAllLocation()
        {
            return ResultWrap(new LocationRal(_tenant).GetAllLocation, "Location");
        }

        public Result GetLocationById(int id)
        {
            return ResultWrap(new LocationRal(_tenant).GetLocationDetailById, id, "Location");
        }

        private Drawer CreateDrawer()
        {
            return new Drawer() { DrawerName = "Drawer1" };
        }
        private List<Bay> CreateBay()
        {
            List<Bay> bay = new List<Bay>();
            bay.Add(new Bay() { BayName = "Bay1", IsActive = true, IsDeleted = false });
            bay.Add(new Bay() { BayName = "Bay2", IsActive = true, IsDeleted = false });
            bay.Add(new Bay() { BayName = "Bay3", IsActive = true, IsDeleted = false });
            return bay;
        }
    }
}
