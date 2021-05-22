using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class BillingDetail
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string Postal { get; set; }

    }
}
