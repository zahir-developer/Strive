using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.CheckoutEntry
{
    public class CheckoutEntryDto
    {
        public int id { get; set; }
        public bool CheckOut { get; set; }
        public DateTime ActualTimeOut { get; set; }
    }
}
