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
        [Route("GetAll")]
        public Result GetAllProduct()
        {
            return _ProductBpl.GetProductDetails();
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveEmployee(Product product)
        {
            return _ProductBpl.SaveProduct(product);
        }
    }
}