using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ProductVendorViewModel
    {
        public int ProductVendorId { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
}
