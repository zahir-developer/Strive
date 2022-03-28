using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Report
{
   public class IrregularitiesDto
    {
        public int LocationId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
