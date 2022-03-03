using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class JobsGridDTO
    {
        public DateTime? JobDate { get; set; }
        public int? LocationId { get; set; }
        public int? ClientId { get; set; }
        public string JobType { get; set; }
    }
}
