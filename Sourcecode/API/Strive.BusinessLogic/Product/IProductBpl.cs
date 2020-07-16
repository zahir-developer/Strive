using Strive.BusinessEntities;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic
{
    public interface IProductBpl
    {
        Result GetAllProduct();

        Result SaveProduct(List<Product> products);

        Result GetProduct(int productId);

        Result DeleteProduct(int productId);
    }
}
