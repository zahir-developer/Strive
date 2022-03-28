using Strive.BusinessEntities.DTO.Sales;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Sales
{
    public interface ISalesBpl
    {
        Result SaveProductItem(SalesProductItemDto salesProductItemDto);
        Result UpdateItem(SalesItemUpdateDto salesItemUpdateDto);
        Result DeleteItemById(DeleteItemDto itemDto);
        Result GetItemList(SalesListItemDto salesListItemDto);
        Result GetAccountDetails(SalesAccountDto salesAccountDto);
        Result GetScheduleByTicketNumber(SalesDto salesDto);
        Result AddPayment(SalesPaymentDetailDto salesPayment);
        Result AddListItem(SalesAddListItemDto salesAddListItem);
        Result RollBackPayment(SalesItemDeleteDto salesItemDeleteDto);
        Result DeleteJob(SalesItemDeleteDto salesItemDeleteDto);
        Result UpdateListItem(SalesUpdateItemDto salesUpdateItemDto);
        Result GetServicesWithPrice();
        Result GetServicesAndProduct(int id, string query);
        Result GetTicketsByPaymentId(int id);
        Result AddTipPayment(TipPaymentDTO salesPayment);
    }
}
