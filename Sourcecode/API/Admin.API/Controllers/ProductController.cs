using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities.Model;
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
        public Result Add([FromBody] Product product) => _bplManager.AddProduct(product);


        [HttpPost]
        [Route("Update")]
        public Result Update([FromBody] Product product) => _bplManager.UpdateProduct(product);


        [HttpDelete]
        [Route("Delete")]
        public Result DeleteProduct(int productId, string fileName = null) => _bplManager.DeleteProduct(productId, fileName);

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllProduct() => _bplManager.GetAllProduct();

        [HttpGet]
        [Route("GetProductById")]
        public Result GetProduct(int productId) => _bplManager.GetProduct(productId);
    }
}