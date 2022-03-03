using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class PaymentAuthRequest
    {
        [JsonProperty("cardConnect")]
        public object CardConnect { get; set; }

        [JsonProperty("paymentDetail")]
        public PaymentDetail PaymentDetail { get; set; }

        [JsonProperty("billingDetail")]
        public BillingDetail BillingDetail { get; set; } 
    }
}
