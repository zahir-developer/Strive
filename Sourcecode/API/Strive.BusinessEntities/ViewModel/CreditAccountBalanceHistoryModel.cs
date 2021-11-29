using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CreditAccountBalanceHistoryModel
    {
        public CreditAccountBalanceViewModel GiftCardBalanceViewModel { get; set; }
        public List<CreditAccountHistoryViewModel> GiftCardHistoryViewModel { get; set; }
    }
}
