using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
   public class IrregularitiesViewModel
    {
        public List<DepositOffViewModel> DepositOff { get; set; }
        public List<IrregularityViewModel> VehiclesInfo { get; set; }
        public List<IrregularityViewModel> MissingTicket { get; set; }
        public List<IrregularityViewModel> Coupon { get; set; }

    }
}
