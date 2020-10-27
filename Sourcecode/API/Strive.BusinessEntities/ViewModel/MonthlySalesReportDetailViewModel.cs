using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class MonthlySalesReportDetailViewModel
    {
        public List<EmployeeInputViewModel> EmployeeViewModel { get; set; }
        public List<MonthlySalesReportViewModel> MonthlySalesReportViewModel { get; set; }
    }
}
