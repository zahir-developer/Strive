using System;
using System.Collections.Generic;
using System.Text;

using Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.ViewModel.Product
{
    public class ProductsDto
    {
        public Model.Product Product { get; set; }
        public List<ProductVendor> ProductVendor { get; set; }

    }
}
