using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Checkout
{
    public class JobCompleteDto
    {
        public int JobId { get; set; }

        public DateTimeOffset ActualTimeOut { get; set; }
    }
}
