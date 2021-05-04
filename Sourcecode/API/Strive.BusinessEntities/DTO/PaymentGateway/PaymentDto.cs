using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class PaymentDto
    {
        public string Account { get; set; }
        public string Expiry { get; set; }
        public string Amount { get; set; }
        public string OrderId { get; set; }
        public string Batchid { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Capture { get; set; }
        public string Receipt { get; set; }
        public string CCV { get; set; }
    }
}
