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
        public EmployeeDto Employee { get; set; }
        public List<EmployeeDocumentDto> EmployeeDocument { get; set; }
        public List<EmployeeLiabilityDto> EmployeeCollision { get; set; }
        public List<EmployeeRoleDto> EmployeeRoles { get; set; }
        public List<EmployeeLocationDto> EmployeeLocations { get; set; }
    }
}
