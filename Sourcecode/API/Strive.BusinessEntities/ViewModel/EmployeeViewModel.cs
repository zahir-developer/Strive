using Strive.BusinessEntities.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel.Employee
{
    public class EmployeeViewModel 
    {
        public List<EmployeeDto> Employee { get; set; }
        public CountViewModel Count { get; set; }
    }
}
