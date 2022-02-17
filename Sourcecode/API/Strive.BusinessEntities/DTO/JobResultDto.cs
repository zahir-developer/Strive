using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class JobResultDto
    {
        public int? JobId { get; set; }
        public string TicketNumber { get; set; }
        public bool Status { get; set; }
    }
}
