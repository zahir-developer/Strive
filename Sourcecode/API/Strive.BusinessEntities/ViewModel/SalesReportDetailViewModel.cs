using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class SalesReportDetailViewModel
    {
        public List<MonthlySalesInputViewModel> EmployeeViewModel { get; set; }
        public List<SalesReportViewModel> MonthlySalesReportViewModel { get; set; }
    }
}
