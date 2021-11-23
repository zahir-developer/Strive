using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class BillingDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }
    }
}
