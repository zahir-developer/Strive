using Newtonsoft.Json.Linq;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class CaptureDetail
    {
        public string AuthCode { get; set; }

        public string Amount { get; set; }

        public string RetRef { get; set; }

        public JToken InvoiceId { get; set; }
        public int LocationId { get; set; }
    }
}