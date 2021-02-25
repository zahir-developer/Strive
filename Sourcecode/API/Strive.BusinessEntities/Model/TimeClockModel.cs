using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using Strive.BusinessEntities.DTO.TimeClock;

namespace Strive.BusinessEntities.Model
{
    public class TimeClockModel
    {
        public List<TimeClock> TimeClock { get; set; }

       public TimeClockWeekDetailDto TimeClockWeekDetailDto { get; set; }
    }
}
