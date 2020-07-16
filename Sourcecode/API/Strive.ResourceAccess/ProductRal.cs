using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Strive.ResourceAccess
{

    public class ProductRal
    {
        private Db db;

        public ProductRal(ITenantHelper tenant)
        {
            var dbConnection = tenant.db();
            db = new Db(dbConnection);
        }

        public List<Product> GetProductDetails()
        {
            return db.Fetch<Product>(SPEnum.USPGETAllPRODUCT.ToString(), null);
        }

        public Product GetProduct(int productId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductId", productId, DbType.Int32, ParameterDirection.Input);
            return db.FetchFirstResult<Product>(SPEnum.USPGETPRODUCT.ToString(), parameters);
        }

        public int SaveProduct(List<Product> products)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tvpProduct", products.ToDataTable().AsTableValuedParameter());
            return db.Execute<Product>("USPSaveProduct".ToString(), parameters);
        }

        public int DeleteProduct(int productId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductId", productId, DbType.Int32, ParameterDirection.Input);
            return db.Execute<int>(SPEnum.USPDELETEPRODUCT.ToString(), parameters);
        }
    }
}
