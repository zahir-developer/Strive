using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class EmployeeTipViewModel
    {
        public int LocationId { get; set; }
        public string Locationname { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
        public decimal TotalHours { get; set; }
        public decimal HoursPerDay { get; set; }
        public decimal Tip { get; set; }
    }
}
