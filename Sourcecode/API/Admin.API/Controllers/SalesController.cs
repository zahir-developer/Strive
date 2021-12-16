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


        [HttpPost]
        [Route("SaveProductItem")]
        public Result SaveProductItem([FromBody] SalesProductItemDto salesProductItemDto)
        {
            return _bplManager.SaveProductItem(salesProductItemDto);
        }

        [HttpPost]
        [Route("UpdateItem")]
        public Result UpdateItem([FromBody] SalesItemUpdateDto salesItemUpdateDto)
        {
            return _bplManager.UpdateItem(salesItemUpdateDto);
        }

        [HttpDelete]
        [Route("DeleteItemById")]
        public Result DeleteItemById(DeleteItemDto itemDto)
        {
            return _bplManager.DeleteItemById(itemDto);
        }
        

        [HttpPost]
        [Route("GetItemList")]
        public Result GetItemList([FromBody] SalesListItemDto salesListItemDto) => _bplManager.GetItemList(salesListItemDto);


        [HttpPost]
        [Route("GetAccountDetails")]
        public Result GetAccountDetails([FromBody] SalesAccountDto salesAccountDto) => _bplManager.GetAccountDetails(salesAccountDto);
        

        [HttpPost]
        [Route("GetScheduleByTicketNumber")]
        public Result GetScheduleByTicketNumber([FromBody] SalesDto salesDto) => _bplManager.GetScheduleByTicketNumber(salesDto);

        [HttpPost]
        [Route("AddPayment")]
        public Result AddPayment([FromBody] SalesPaymentDetailDto salesPayment) => _bplManager.AddPayment(salesPayment);

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
        [Route("DeleteJob")]
        public Result DeleteJob(SalesItemDeleteDto salesItemDeleteDto)
        {
            return _bplManager.DeleteJob(salesItemDeleteDto);
        }

        [HttpPost]
        [Route("RollBackPayment")]
        public Result RollBackPayment([FromBody] SalesItemDeleteDto salesItemDeleteDto)
        {
            return _bplManager.RollBackPayment(salesItemDeleteDto);
        }
        [HttpGet]
        [Route("GetAllServiceAndProductList/{id}/{query}")]
        public Result GetServicesAndProduct(int id, string query)
        {
            return _bplManager.GetServicesAndProduct(id,  query);
        }
        [HttpGet]
        [Route("GetTicketsByPaymentId/{id}")]
        public Result GetTicketsByPaymentId(int id)
        {
            return _bplManager.GetTicketsByPaymentId(id);
        }
    }
}