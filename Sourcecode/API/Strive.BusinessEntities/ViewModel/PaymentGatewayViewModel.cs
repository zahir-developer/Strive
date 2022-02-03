using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PaymentGatewayViewModel
    {
        public int PaymentGatewayId { get; set; }
        public string PaymentGatewayName { get; set; }
        public string BaseURL { get; set; }
        public string APIKey { get; set; }
    }
}
