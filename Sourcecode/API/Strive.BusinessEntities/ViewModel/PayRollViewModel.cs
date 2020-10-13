using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PayRollViewModel
    {
        public PayRollRateViewModel PayRollRateViewModel { get; set; }
        public List<PayRollCategoryViewModel> PayRollCategoryViewModel { get; set; }
    }
}
