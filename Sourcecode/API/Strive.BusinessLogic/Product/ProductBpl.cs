using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;

namespace Strive.BusinessLogic
{
    public class ProductBpl : Strivebase, IProductBpl
    {
        public ProductBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result AddProduct(Product product)
        {
            return ResultWrap(new ProductRal(_tenant).AddProduct, product, "Status");
        }

        public Result UpdateProduct(Product product)
        {
            return ResultWrap(new ProductRal(_tenant).UpdateProduct, product, "Status");
        }

        public Result GetAllProduct()
        {
            return ResultWrap(new ProductRal(_tenant).GetAllProduct, "Product");
        }

        public Result GetProduct(int productId)
        {
            return ResultWrap(new ProductRal(_tenant).GetProductById, productId, "Product");
        }

        public Result DeleteProduct(int productId)
        {
            try
            {
                var success = new ProductRal(_tenant).DeleteProduct(productId);
                _resultContent.Add(success.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

    }
}
