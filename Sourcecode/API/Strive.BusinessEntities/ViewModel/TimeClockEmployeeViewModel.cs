using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TimeClockEmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal WashHours { get; set; }
        public decimal DetailHours { get; set; }
        public decimal OtherHours { get; set; }
        public decimal TotalHours { get; set; }
    }
}
