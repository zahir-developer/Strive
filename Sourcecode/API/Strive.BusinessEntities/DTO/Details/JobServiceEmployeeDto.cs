using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Details
{
    public class JobServiceEmployeeDto
    {
        public List<JobServiceEmployee> JobServiceEmployee { get; set; }

        public int? JobId { get; set; }
    }
}
