using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class PaymentCaptureReq
    {
        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("retRef")]
        public string RetRef { get; set; }

        [JsonProperty("invoiceId")]
        public object InvoiceID { get; } //= new();
    }
}
