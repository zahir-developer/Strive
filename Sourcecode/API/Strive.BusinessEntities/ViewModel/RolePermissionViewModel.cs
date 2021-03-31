using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class RolePermissionViewModel
    {
        public int EmployeeID { get; set; }      
        public int ClientId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
        public string ViewName { get; set; }
        public string FieldName { get; set; }

    }
}
