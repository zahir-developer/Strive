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
            return db.Fetch<Product>(SPEnum.USPGETAllPRODUCT.ToString(),null);
        }

        public Product GetProduct(int productId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductId", productId, DbType.Int32, ParameterDirection.Input);
            return db.FetchFirstResult<Product>(SPEnum.USPGETPRODUCT.ToString(), parameters);
        }

        public int AddProduct(Product product)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductName", product.ProductName, DbType.String, ParameterDirection.Input);
            parameters.Add("ProductType", product.ProductType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("LocationId", product.LocationId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("VendorId", product.VendorId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Size", product.Size, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SizeDescription", product.SizeDescription, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Quantity", product.Quantity, DbType.Double, ParameterDirection.Input);
            parameters.Add("QuantityDescription", product.ProductType, DbType.String, ParameterDirection.Input);
            parameters.Add("Cost", product.ProductType, DbType.Double, ParameterDirection.Input);
            parameters.Add("IsTaxable", product.ProductType, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("TaxAmount", product.TaxAmount, DbType.Double, ParameterDirection.Input);
            parameters.Add("IsActive", product.Quantity, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("ThresholdLimit", product.Quantity, DbType.Int32, ParameterDirection.Input);

            return db.Execute<Product>(SPEnum.USPGETPRODUCT.ToString(), parameters);
        }

        public int DeleteProduct(int productId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductId", productId, DbType.Int32, ParameterDirection.Input);
            return db.Execute<int>(SPEnum.USPDELETEPRODUCT.ToString(), parameters);
        }

        public int UpdateProduct(Product product)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductId", product.ProductId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("ProductName", product.ProductName, DbType.String, ParameterDirection.Input);
            parameters.Add("ProductType", product.ProductType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("LocationId", product.LocationId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("VendorId", product.VendorId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Size", product.Size, DbType.Int32, ParameterDirection.Input);
            parameters.Add("SizeDescription", product.SizeDescription, DbType.Int32, ParameterDirection.Input);
            parameters.Add("Quantity", product.Quantity, DbType.Double, ParameterDirection.Input);
            parameters.Add("QuantityDescription", product.ProductType, DbType.String, ParameterDirection.Input);
            parameters.Add("Cost", product.ProductType, DbType.Double, ParameterDirection.Input);
            parameters.Add("IsTaxable", product.ProductType, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("TaxAmount", product.TaxAmount, DbType.Double, ParameterDirection.Input);
            parameters.Add("IsActive", product.Quantity, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("ThresholdLimit", product.Quantity, DbType.Int32, ParameterDirection.Input);

            return db.Execute<Product>(SPEnum.USPDELETEPRODUCT.ToString(), parameters);
        }
    }
}
