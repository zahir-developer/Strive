using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class PaymentAuthReq
    {
        [JsonProperty("cardConnect")]
        public object CardConnect { get; set; } //= new();

        [JsonProperty("paymentDetail")]
        public PaymentDetail PaymentDetail { get; set; }

        [JsonProperty("billingDetail")]
        public BillingDetail BillingDetail { get; set; } //= new();

        //[JsonProperty("isRepeatTransaction")]
        //public bool isRepeatTransaction { get; } = false;
    }
}
