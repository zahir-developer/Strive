using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace Strive.BusinessEntities.Model
{
    public class TimeClockModel
    {
        public List<TimeClock> TimeClock { get; set; }
    }
}
