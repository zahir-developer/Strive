using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Strive.ResourceAccess
{

    public class ProductRal : RalBase
    {
        public ProductRal(ITenantHelper tenant) : base(tenant) { }

        public bool AddProduct(Product product)
        {
            return dbRepo.Insert(product);
        }

        public bool UpdateProduct(Product product)
        {
            return dbRepo.Update(product);
        }

        public Product GetProductById(int productId)
        {
            _prm.Add("@ProductId", productId);
            return db.FetchSingle<Product>(SPEnum.USPGETPRODUCTS.ToString(), _prm);
        }

        public List<Product> GetAllProduct()
        {
            return db.Fetch<Product>(SPEnum.USPGETPRODUCTS.ToString(), null);
        }

        public int DeleteProduct(int productId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductId", productId, DbType.Int32, ParameterDirection.Input);
            return db.Execute<int>(SPEnum.USPDELETEPRODUCT.ToString(), parameters);
        }
    }
}
