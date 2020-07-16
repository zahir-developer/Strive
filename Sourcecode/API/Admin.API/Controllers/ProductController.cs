using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ProductController : ControllerBase
    {
        IProductBpl _ProductBpl = null;

        public ProductController(IProductBpl ProductBpl)
        {
            _ProductBpl = ProductBpl;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public Result GetAllProduct()
        {
            return _ProductBpl.GetAllProduct();
        }

        [HttpGet]
        [Route("GetProduct/{productId}")]
        public Result GetProduct(int productId)
        {
            return _ProductBpl.GetProduct(productId);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveEmployee([FromBody] List<Product> products)
        {
            return _ProductBpl.SaveProduct(products);
        }

        [HttpDelete]
        [Route("DeleteProduct/{productId}")]
        public Result DeleteProduct(int productId)
        {
            return _ProductBpl.DeleteProduct(productId);
        }


    }
}