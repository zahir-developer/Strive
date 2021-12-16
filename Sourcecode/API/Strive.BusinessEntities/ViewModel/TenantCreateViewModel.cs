using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TenantCreateViewModel
    {
        public TenantViewModel TenantViewModel { get; set; }
        public List<TenantListModuleViewModel> Module { get; set; }
        //public ModuleListDto TenantModuleViewModel { get; set; }
    }
}
