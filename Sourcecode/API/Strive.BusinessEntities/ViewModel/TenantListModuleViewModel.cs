using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TenantListModuleViewModel
    {
        public TenantModuleViewModel Module { get; set; }
        public List<TenantModuleScreenViewModel> ModuleScreen { get; set; }
        public List<TenantMobileApp> MobileApp { get; set; }


    }
}
