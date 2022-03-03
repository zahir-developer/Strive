using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class PayAuthResp: BaseResponsePayment
    {
        [JsonProperty("authcode")]
        public string Authcode { get; set; }

        [JsonProperty("retref")]
        public string Retref { get; set; }

        [JsonProperty("respstat")]
        public string SucessType { get; set; }

        [JsonProperty("resptext")]
        public string ErrorMessage { get; set; }

    }
}
