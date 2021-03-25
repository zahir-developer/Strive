using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Product;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Product;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic
{
    public interface IProductBpl
    {
        Result AddProduct(ProductAddDto product);
        Result UpdateProduct(Product product);
        Result GetAllProduct();
        Result GetProduct(int productId);
        Result DeleteProduct(int productId, string fileName = null);
        Result GetProductSearch(ProductSearchDto search);
    }
}
