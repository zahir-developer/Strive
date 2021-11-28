using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;

namespace Greeter.Modules.Home
{
    public partial class WashTimeViewController
    {
        List<Location> locations = new();

        //string scannedCardData = "%B4799320385017982^K LAKSHMANA/^280722600722000000?;4799320385017982=2807226722?(null)";

        public WashTimeViewController()
        {
            //string cardNo = ParseMagcardData(scannedCardData);
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

        private static string ParseMagcardData(string rawData)
        {
            Regex magcardRegex = new Regex(@"^%B(\d+)\^");
            Match numberMatch = magcardRegex.Match(rawData);
            if (!numberMatch.Success)
                return null;
            return numberMatch.Groups[1].Value;
        }
    }
}