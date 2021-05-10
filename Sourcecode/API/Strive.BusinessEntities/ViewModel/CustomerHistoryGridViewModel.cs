using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
   public class CustomerHistoryGridViewModel
    {
        public List<CustomerHistoryViewModel> customerHistoryViewModel { get; set; }
        public CountViewModel Count { get; set; }
    }
}
