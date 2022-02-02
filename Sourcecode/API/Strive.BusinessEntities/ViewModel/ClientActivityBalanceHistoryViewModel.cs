﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientActivityBalanceHistoryViewModel
    {
        public List<ClientActivityHistoryViewModel> ClientActivityHistoryViewModel { get; set; }

        public List<ClientActivityStatementViewModel> ClientActivityStatementViewModel { get; set; }

        public List<ClientActivityPaymentHistoryViewModel> ClientActivityPaymentHistoryViewModel { get; set; }
      
        public List<ClientAccountViewModel> CreditAmount { get; set; }
    }
}