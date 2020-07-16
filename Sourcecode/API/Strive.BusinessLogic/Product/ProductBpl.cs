﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Net;

namespace Strive.BusinessLogic
{
    public class ProductBpl : Strivebase, IProductBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public ProductBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }

        public Result GetProductDetails()
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

        public Result AddProduct(Product product)
        {
            try
            {
                var success = new ProductRal(_tenant).AddProduct(product);
                _resultContent.Add(success.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveProduct(Product product)
        {
            try
            {
                var success = new ProductRal(_tenant).UpdateProduct(product);
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
