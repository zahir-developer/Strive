using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Washes
{
    public class WashesDashboardDto
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int JobType { get; set; }
    }
}
