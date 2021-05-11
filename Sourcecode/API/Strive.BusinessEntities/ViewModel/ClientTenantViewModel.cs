using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientTenantViewModel
    {
        public int TenantId { get; set; }
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public int? MaxLocation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientEmail { get; set; }
        public string ZipCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? SubscriptionDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool? IsActive { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
    }
}
