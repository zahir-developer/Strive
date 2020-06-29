using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic
{
    public interface IProductBpl
    {
        Result GetProductDetails();

        Result AddProduct(Product product);

        Result SaveProduct(Product products);

        Result GetProduct(int productId);

        //Result DeleteProduct(int productId);
    }
}
