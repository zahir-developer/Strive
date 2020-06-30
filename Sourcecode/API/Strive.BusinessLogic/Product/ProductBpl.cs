using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic
{
    public class ProductBpl : Strivebase, IProductBpl
    {
        ITenantHelper tenant;
        JObject result = new JObject();

        ProductRal productRal;
        public ProductBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
           
        }

        public Result GetProductDetails()
        {
            try
            {
                var list = new ProductRal(tenant).GetProductDetails(new Dapper.DynamicParameters());

                result.Add(list.WithName("Product"));

                return Helper.BindSuccessResult(result);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Result GetProduct(int productId)
        {
            try
            {
                var product = productRal.GetProduct(productId);

                result.Add(product);

                return Helper.BindSuccessResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result AddProduct(Product product)
        {
            try
            {
                var success = productRal.AddProduct(product);

                result.Add(success.WithName("Status"));

                return Helper.BindSuccessResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result SaveProduct(Product product)
        {
            try
            {
                var success = productRal.UpdateProduct(product);

                result.Add(success.WithName("Status"));

                return Helper.BindSuccessResult(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
