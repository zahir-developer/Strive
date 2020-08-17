using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Product;
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

        public List<Product> GetAllProduct()
        {
            return db.Fetch<Product>(SPEnum.USPGETPRODUCTS.ToString(), null);
        }

        public ProductDetailViewModel GetProductById(int productId)
        {
            _prm.Add("@ProductId", productId);
            return db.FetchSingle<ProductDetailViewModel>(SPEnum.USPGETPRODUCTS.ToString(), _prm);
        }

        public bool DeleteProduct(int productId)
        {
            _prm.Add("ProductId", productId);
            db.Save(SPEnum.USPDELETEPRODUCT.ToString(), _prm);
            return true;
        }
    }
}
