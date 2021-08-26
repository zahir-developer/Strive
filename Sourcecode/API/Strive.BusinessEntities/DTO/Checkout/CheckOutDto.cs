using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Checkout
{
   public class CheckOutDto
    {
        public int JobId { get; set; }
        public bool CheckOut { get; set; }
        public DateTimeOffset CheckOutTime { get; set; }
    }

}
