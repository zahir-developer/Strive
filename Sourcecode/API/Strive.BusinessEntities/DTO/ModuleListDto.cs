using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.ViewModel
{
    public class ModuleListDto
    {
        public List<TenantModuleViewModel> Module { get; set; }
        public List<TenantModuleScreenViewModel> ModuleScreen { get; set; }
        public List<TenantMobileAppViewModel> MobileApp { get; set; }

    }
}
