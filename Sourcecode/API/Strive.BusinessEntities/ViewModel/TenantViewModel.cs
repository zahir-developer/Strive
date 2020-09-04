using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TenantViewModel
    {
        public int SubscriptionId { get; set; }
        public string TenantName { get; set; }
        public string PasswordHash { get; set; }
        public string TenantEmail { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
