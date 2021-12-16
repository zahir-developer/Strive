using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Sales
{
    public class SalesPaymentDto
    {
        public JobPayment JobPayment { get; set; }
        public List<JobPaymentDetail> JobPaymentDetail { get; set; }
        public List<GiftCardHistory> GiftCardHistory { get; set; }
        public CreditAccountHistory CreditAccountHistory { get; set; }
        public JobPaymentCreditCard JobPaymentCreditCard { get; set; }
    }
}