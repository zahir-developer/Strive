using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TenantModuleScreenViewModel
    {
        public int ModuleScreenId { get; set; }
        public int ModuleId { get; set; }
        public string ViewName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
