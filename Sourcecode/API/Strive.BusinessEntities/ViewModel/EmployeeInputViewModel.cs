using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class EmployeeInputViewModel
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTimeOffset? FirstLoginDate { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}
