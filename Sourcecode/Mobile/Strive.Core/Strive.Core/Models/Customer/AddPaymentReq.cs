using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class AddPaymentReq
    {
        [JsonProperty("SalesPaymentDto")]
        public SalesPaymentDto SalesPaymentDto { get; set; }

        //[JsonProperty("jobPaymentDetail")]
        //public List<JobPaymentDetail> JobPaymentDetails { get; set; }

        [JsonProperty("locationId")]
        public long LocationID { get; set; }

        [JsonProperty("jobId")]
        public long JobID { get; set; }

        [JsonProperty("ticketNumber")]
        public string TicketNumber { get; set; }
    }
}
