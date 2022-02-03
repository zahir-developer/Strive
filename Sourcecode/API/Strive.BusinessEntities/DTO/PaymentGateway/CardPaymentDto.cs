using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class CardPaymentDto
    {
        public CardConnectDetail CardConnect { get; set; }

        public PaymentDetail PaymentDetail { get; set; }

        public BillingDetail BillingDetail { get; set; }

        public bool IsRepeatTransaction { get; set; }
       public int LocationId { get; set; }
    }
}
