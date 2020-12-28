using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class SalesItemListViewModel
    {
        public List<SalesItemViewModel> SalesItemViewModel { get; set; }
        public List<ProductItemViewModel> ProductItemViewModel { get; set; }
        public List<SalesSummaryViewModel> SalesSummaryViewModel { get; set; }
        public List<PaymentStatusViewModel> PaymentStatusViewModel { get; set; }

    }
}