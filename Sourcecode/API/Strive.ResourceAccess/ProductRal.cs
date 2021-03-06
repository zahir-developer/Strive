using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Product;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
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

        public int AddProduct(ProductsDto product)
        {
            return dbRepo.InsertPK(product,"ProductId");
        }

        public bool UpdateProduct(ProductsDto product)
        {
            return dbRepo.UpdatePc(product);
        }

        public List<ProductViewModel> GetAllProduct(ProductSearchDto search)
        {
            _prm.Add("@ProductSearch", search.ProductSearch);
            _prm.Add("@ProductTypeNames", search.ProductTypeNames);
            return db.Fetch<ProductViewModel>(EnumSP.Product.USPGETPRODUCTS.ToString(), _prm);
        }

        public ProductDetailViewModel GetProductById(int? productId)
        {
            _prm.Add("@ProductId", productId);
            return db.FetchSingle<BusinessEntities.ViewModel.ProductDetailViewModel>(EnumSP.Product.USPGETPRODUCTBYID.ToString(), _prm);
        }

        public ProductEditViewModel GetProductDetailById(int? productId)
        {
            _prm.Add("@ProductId", productId);
            return db.FetchMultiResult<ProductEditViewModel>(EnumSP.Product.USPGETPRODUCTDETAILBYID.ToString(), _prm);
        }

        public bool DeleteProduct(int productId)
        {
            _prm.Add("ProductId", productId);
            db.Save(EnumSP.Product.USPDELETEPRODUCT.ToString(), _prm);
            return true;
        }
        
    }
}
