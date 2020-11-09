using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class EmployeeTipReportDto
    {
        public int month { get; set; }
        public int year { get; set; }
        public int LocationId { get; set; }
        public DateTime? Date { get; set; }
    }
}
