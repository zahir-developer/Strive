using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientTenantViewModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string MobileNumber { get; set; }
        public int TenantId { get; set; }       
        public string ClientEmail { get; set; }
        public string CompanyName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsActive { get; set; }
    }
}
