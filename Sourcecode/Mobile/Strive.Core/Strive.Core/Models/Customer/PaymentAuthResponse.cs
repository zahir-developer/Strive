using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class PayAuthResponse : BaseResponsePayment
    {
        [JsonProperty("authcode")]
        public string Authcode { get; set; }

        [JsonProperty("retref")]
        public string Retref { get; set; }
    }
}
