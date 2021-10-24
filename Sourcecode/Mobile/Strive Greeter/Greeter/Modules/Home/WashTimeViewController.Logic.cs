using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;

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
            var response = await new GeneralApiService().GetLocationWashTime();
            HideActivityIndicator();

            HandleResponse(response);

            if (response.IsSuccess())
            {
                locations = response.Locations;
                PlaceLocationDetailsToMap(locations);
            }
        }
    }
}