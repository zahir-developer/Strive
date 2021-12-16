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

        //string visaCardData = "%B4799320385017982^K LAKSHMANA/^280722600722000000?;4799320385017982=2807226722?(null)";

        //string amexCardData = "%B376537649781009^D/NARENDREN               ^2310201181064102?(null)(null)";

        public WashTimeViewController()
        {
            //    string cardNo = ParseMagcardData(amexCardData);
            //    string expiry = ParseExpiryMonth(amexCardData);
            //    string year = ParseExpiryYear(amexCardData);
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

        //private static string ParseMagcardData(string rawData)
        //{
        //    Regex magcardRegex = new Regex(@"^%B(\d+)\^");
        //    Match numberMatch = magcardRegex.Match(rawData);
        //    if (!numberMatch.Success)
        //        return null;
        //    return numberMatch.Groups[1].Value;
        //}

        //string ParseExpiryYear(string rawData)
        //{
        //    string BEFORE_EXPIRY_MONTH_YEAR = "/^";

        //    int pos = rawData.LastIndexOf("^");

        //    string yearMonth = rawData.Substring(pos + 1, 4);

        //    string year = yearMonth.Substring(0, 2);

        //    return year;
        //}

        //string ParseExpiryMonth(string rawData)
        //{
        //    //string BEFORE_EXPIRY_MONTH_YEAR = "/^";

        //    int pos = rawData.LastIndexOf("^");

        //    string yearMonth = rawData.Substring(pos + 1, 4);

        //    string month = yearMonth.Substring(2, 2);

        //    return month;
        //}
    }
}