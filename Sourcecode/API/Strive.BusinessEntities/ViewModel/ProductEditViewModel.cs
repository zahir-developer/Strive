using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ProductEditViewModel
    {
        public ProductDetailViewModel ProductDetail { get; set; }

        public List<ProductVendorViewModel> ProductVendor { get; set; }
    }
}
