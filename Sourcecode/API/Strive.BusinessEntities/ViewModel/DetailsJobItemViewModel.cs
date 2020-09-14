using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DetailsJobItemViewModel
    {
        public int JobItemId { get; set; }
        public int JobId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceName { get; set; }
    }
}
