using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class JobPaymentDetail
    {
        [JsonProperty("jobPaymentDetailId")]
        public int JobPaymentDetailID { get; } = 0;

        [JsonProperty("jobPaymentId")]
        public int JobPaymentID { get; } = 0;

        [JsonProperty("paymentType")]
        public long PaymentType { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("taxAmount")]
        public float TaxAmount { get; } = 0;

        [JsonProperty("signature")]
        public string Signature { get; } = string.Empty;

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; } = false;

        [JsonProperty("createdBy")]
        public long CreatedBy { get; } // = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        //[JsonProperty("updatedBy")]
        //public long UpdatedBy { get; } = AppSettings.UserID;

        //[JsonProperty("updatedDate")]
        //public DateTime UpdatedDate { get; } = DateTime.Now;
    }
}
