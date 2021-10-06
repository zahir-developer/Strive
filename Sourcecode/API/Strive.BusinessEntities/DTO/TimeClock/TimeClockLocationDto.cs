using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.TimeClock
{
    public class TimeClockLocationDto
    {
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CurrentDate { get; set; }
        
    }
}
