using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class WashDetailViewModel
    {
        public List<WashesViewModel> Washes { get; set; }
        public List<WashItemViewModel> WashItem { get; set; }
    }
}
