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
        [HttpDelete]
        [Route("DeleteItemById")]
        public Result DeleteItemById(int jobItemId)
        {
            return _bplManager.DeleteItemById(jobItemId);
        }
        [HttpGet]
        [Route("GetTicketNumber")]
        public string GetTicketNumber() => _bplManager.GetTicketNumber();

        [HttpPost]
        [Route("GetItemList")]
        public Result GetItemList([FromBody] SalesListItemDto salesListItemDto) => _bplManager.GetItemList(salesListItemDto);

        [HttpGet]
        [Route("GetScheduleByTicketNumber")]
        public Result GetScheduleByTicketNumber(string ticketNumber) => _bplManager.GetScheduleByTicketNumber(ticketNumber);
        [HttpPost]
        [Route("AddPayment")]
        public Result AddPayment([FromBody] SalesPaymentDto salesPayment) => _bplManager.AddPayment(salesPayment);

        [HttpPost]
        [Route("AddListItem")]
        public Result AddListItem([FromBody] SalesAddListItemDto salesAddListItem) => _bplManager.AddListItem(salesAddListItem);
        [HttpPost]
        [Route("UpdateListItem")]
        public Result UpdateListItem([FromBody] SalesUpdateItemDto salesUpdateItemDto) => _bplManager.UpdateListItem(salesUpdateItemDto);
        [HttpGet]
        [Route("GetServicesWithPrice")]
        public Result GetServicesWithPrice() => _bplManager.GetServicesWithPrice();
        [HttpDelete]
        [Route("DeleteTransactions")]
        public Result DeleteTransactions(SalesItemDeleteDto salesItemDeleteDto)
        {
            return _bplManager.DeleteTransactions(salesItemDeleteDto);
        }
        
    }
}