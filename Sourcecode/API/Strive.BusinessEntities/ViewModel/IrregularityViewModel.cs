﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
   public class IrregularityViewModel
    {
        public List<IrregularityDetailViewModel> VehiclesInfo { get; set; }
        public List<IrregularityDetailViewModel> MissingTicket { get; set; }
        public List<IrregularityDetailViewModel> Coupon { get; set; }
        public List<DepositOffViewModel> DepositOff { get; set; }

    }
}