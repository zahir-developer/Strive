using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class EmailBlastDto
    {
       
        public DateTime fromDate { get; set; }

        public DateTime toDate { get; set; }
        public bool? IsMembership { get; set; }
    }
}
