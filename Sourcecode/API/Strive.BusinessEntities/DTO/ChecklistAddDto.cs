using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO
{
    public class ChecklistAddDto
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int WashesCount { get; set; }
    }
}
