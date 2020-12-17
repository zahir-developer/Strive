using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class HourlyWashEmployeeViewModel : EmployeeList
    {
        public int LocationId { get; set; }
        public DateTime JobDate { get; set; }
    }
}
