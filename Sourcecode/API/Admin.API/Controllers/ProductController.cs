using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities.DTO.Product;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Product;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]

    [Route("Admin/[Controller]")]
    public class ProductController : StriveControllerBase<IProductBpl>
    {
        public ProductController(IProductBpl prdBpl) : base(prdBpl) { }

        [HttpPost]
        [Route("Add")]
        public Result Add([FromBody] ProductListDto product) => _bplManager.AddProduct(product);


        [HttpPost]
        [Route("Update")]
        public Result Update([FromBody] ProductListDto product) => _bplManager.UpdateProduct(product);


        [HttpDelete]
        [Route("Delete")]
        public Result DeleteProduct(int productId, string fileName = null) => _bplManager.DeleteProduct(productId, fileName);

        [HttpPost]
        [Route("GetAll")]
        public Result GetAllProduct([FromBody] ProductSearchDto search) => _bplManager.GetAllProduct(search);

        [HttpGet]
        [Route("GetProductById")]
        public Result GetProduct(int productId) => _bplManager.GetProduct(productId);

        [HttpGet]
        [Route("GetProductDetailById")]
        public Result GetProductDetail(int productId) => _bplManager.GetProductDetail(productId);

        [HttpPost]
        [Route("UpdateProductQuantity")]
        public Result UpdateProductQuantity(ProductQuantityDto productQuantityDto) => _bplManager.UpdateProductQuantity(productQuantityDto);

        [HttpPost]
        [Route("GetAllProductImage")]
        public Result GetAllProductImage([FromBody] ProductSearchDto search) => _bplManager.GetAllProductAndImage(search);

        [HttpPost]
        [Route("ProductRequest")]
        public Result ProductRequest([FromBody]ProductRequestDto productRequestDto) => _bplManager.ProductRequest(productRequestDto);



    }
}