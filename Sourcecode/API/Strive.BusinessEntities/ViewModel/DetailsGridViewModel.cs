using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class DetailsGridViewModel
    {
       public List<BayDetailViewModel> BayDetailViewModel { get; set; }
       public List<BayJobDetailViewModel> BayJobDetailViewModel { get; set; }
    }
}
