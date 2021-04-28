using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TenantByIdViewModel
    {
        public ClientTenantViewModel TenantViewModel { get; set; }
        public List<TenantModuleViewModel> TenantModuleViewModel { get; set; }
    }
}
