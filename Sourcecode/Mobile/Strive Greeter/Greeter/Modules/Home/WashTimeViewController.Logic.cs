using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Network;

namespace Greeter.Modules.Home
{
    public partial class WashTimeViewController
    {
        List<Location> locations = new();

        public WashTimeViewController()
        {
        }

        async Task GetData()
        {
            ShowActivityIndicator();
            var response = await new ApiService(new NetworkService()).GetLocations();
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                locations = response.Locations;
                PlaceLocationDetailsToMap(locations);
            }
        }
    }
}