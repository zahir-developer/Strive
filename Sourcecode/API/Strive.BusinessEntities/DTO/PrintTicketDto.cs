using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class PrintTicketDto
    {
        public PrintJobDto Job { get; set; }

        public List<PrintJobItemDto> JobItem { get; set; }
    }
}
