using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class PaymentDetail
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("expiry")]
        public string Expiry { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("orderId")]
        public string OrderID { get; set; }

        //[JsonProperty("batchid")]
        //public long BatchID { get; set; }

        //[JsonProperty("currency")]
        //public string Currency { get; set; }

        //[JsonProperty("receipt")]
        //public string Receipt { get; set; }

        //[JsonProperty("ccv")]
        //public short CCV { get; set; }
    }
}
