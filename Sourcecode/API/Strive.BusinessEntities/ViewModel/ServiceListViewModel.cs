using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ServiceListViewModel
    {
        public int? ServiceId { get; set; }
        public int? ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
    }
}
