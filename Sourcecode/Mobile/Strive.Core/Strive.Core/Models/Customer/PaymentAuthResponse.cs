using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class PayAuthResponse : BaseResponsePayment
    {
        [JsonProperty("acctid")]
        public string AccountId { get; set; }

        [JsonProperty("profileid")]
        public string ProfileId { get; set; }


    }
}
