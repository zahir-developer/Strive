using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Product
{
    public class ProductRequestDto
    {
        public  int locationId { get; set; }
        public string locationName { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public decimal RequestQuantity { get; set; }
    }
}
