using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
   public class ProfilePaymentDto
    {
        public string Profile { get; set; }

        public decimal Amount { get; set; }

        public int LocationId { get; set; }
    }
}
