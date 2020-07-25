using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;

namespace Strive.BusinessLogic
{
    public class ProductBpl : Strivebase, IProductBpl
    {
        public ProductBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper,cache)
        {
        }

        public Result GetAllProduct()
        {
            try
            {
                var list = new ProductRal(_tenant).GetProductDetails();
                _resultContent.Add(list.WithName("Product"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }

        public Result GetProduct(int productId)
        {
            try
            {
                var product = new ProductRal(_tenant).GetProduct(productId);
                _resultContent.Add(product.WithName("Product"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveProduct(List<Product> product)
        {
            try
            {
                var success = new ProductRal(_tenant).SaveProduct(product);
                _resultContent.Add(success.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
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
