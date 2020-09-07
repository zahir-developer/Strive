using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Sales;
using Strive.BusinessLogic.Sales;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class SalesController : StriveControllerBase<ISalesBpl>
    {
        public SalesController(ISalesBpl colBpl) : base(colBpl) { }

        //[HttpPost]
        //[Route("UpdateSalesItem")]
        //public Result UpdateSalesItem([FromBody] SalesDto sales) => _bplManager.UpdateSalesItem(sales);

        [HttpPut]
        [Route("UpdateItem")]
        public Result UpdateItem(SalesItemUpdateDto salesItemUpdateDto)
        {
            return _bplManager.UpdateItem(salesItemUpdateDto);
        }
        [HttpPut]
        [Route("DeleteItemById")]
        public Result DeleteItemById(int serviceId)
        {
            return _bplManager.DeleteItemById(serviceId);
        }
        [HttpGet]
        [Route("GetTicketNumber")]
        public string GetTicketNumber() => _bplManager.GetTicketNumber();
    }
}