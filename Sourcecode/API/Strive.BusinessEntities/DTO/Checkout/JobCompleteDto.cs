using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Details
{
    public class JobStatusDto
    {
        public int JobId { get; set; }

        public DateTimeOffset ActualTimeOut { get; set; }

        public int? JobStatusId { get; set; }

        public string JobStatus { get; set; }
    }
}
