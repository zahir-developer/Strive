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
        public SalesSummaryViewModel SalesSummaryViewModel { get; set; }
        public PaymentStatusViewModel PaymentStatusViewModel { get; set; }
        public List<JobDetailViewModel> JobDetailViewModel { get; set; }

    }
}