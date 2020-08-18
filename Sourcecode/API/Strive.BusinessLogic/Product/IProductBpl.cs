﻿using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.Model;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic
{
    public interface IProductBpl
    {
        Result AddProduct(Product product);
        Result UpdateProduct(Product product);
        Result GetAllProduct();
        Result GetProduct(int productId);
        Result DeleteProduct(int productId, string fileName = null);
    }
}
