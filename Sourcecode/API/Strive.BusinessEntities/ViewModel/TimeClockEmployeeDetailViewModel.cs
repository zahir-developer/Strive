using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class TimeClockEmployeeDetailViewModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public DateTime EventDate { get; set; }
    }
}
