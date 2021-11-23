using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class SalesPaymentDto
    {
        [JsonProperty("jobPayment")]
        public JobPayment JobPayment { get; set; }

        [JsonProperty("jobPaymentDetail")]
        public List<JobPaymentDetail> JobPaymentDetails { get; set; }

        [JsonProperty("giftCardHistory")]
        public object GiftCardHistory { get; } = null;

        [JsonProperty("jobPaymentCreditCard")]
        public object JobPaymentCreditCard { get; } = null;
    }
}
