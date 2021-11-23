using System;
using Newtonsoft.Json;

namespace Strive.Core.Models.Customer
{
    public class JobPayment
    {
        [JsonProperty("jobPaymentId")]
        public long JobPaymentID { get; } = 0;

        [JsonProperty("membershipId")]
        public string membershipID { get; } = null;

        [JsonProperty("jobId")]
        public long JobID { get; set; }

        //[JsonProperty("drawerId")]
        //public short DrawerID { get; } = 1;

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("taxAmount")]
        public float TaxAmount { get; } = 0;

        [JsonProperty("approval")]
        public bool Approval { get; } = true;

        [JsonProperty("paymentStatus")]
        public long PaymentStatus { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; } = string.Empty;

        [JsonProperty("isActive")]
        public bool IsActive { get; } = true;

        [JsonProperty("isDeleted")]
        public bool isDeleted { get; } = false;

        [JsonProperty("createdBy")]
        public long CreatedBy { get; }// = AppSettings.UserID;

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; } = DateTime.Now;

        //[JsonProperty("updatedBy")]
        //public long UpdatedBy { get; } = AppSettings.UserID;

        //[JsonProperty("updatedDate")]
        //public DateTime UpdatedDate { get; } = DateTime.Now;

        [JsonProperty("isProcessed")]
        public bool IsProcessed { get; } = true;

        //[JsonProperty("cashback")]
        //public int Cashback { get; } = 0;
    }
}
