using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TenantMobileAppViewModel
    {
        public int MobileAppId { get; set; }
        public string MobileAppName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
