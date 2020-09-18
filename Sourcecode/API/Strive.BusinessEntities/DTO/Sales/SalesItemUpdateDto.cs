using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Sales
{
    public class SalesItemUpdateDto
    {
        public int JobItemId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
    }
}
