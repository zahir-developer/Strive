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
        Result AddProduct(ProductListDto products);
        Result UpdateProduct(ProductListDto products);
        Result GetAllProduct(ProductSearchDto search);
        Result GetProductDetail(int productId);
        Result GetProduct(int productId);
        Result DeleteProduct(int productId, string fileName = null);
        Result GetAllProductAndImage(ProductSearchDto search);

        Result UpdateProductQuantity(ProductQuantityDto productQuantityDto);
        Result ProductRequest(ProductRequestDto productRequestDto);
    }
}
