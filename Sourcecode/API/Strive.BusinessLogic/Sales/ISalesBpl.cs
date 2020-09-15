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
        //Result UpdateSalesItem(SalesDto sales);
        //Result UpdateItem(int jobItemId);
        //Result UpdateItem(int jobItemId, int quantity, decimal price);
        Result UpdateItem(SalesItemUpdateDto salesItemUpdateDto);
        Result DeleteItemById(int jobItemId);
        string GetTicketNumber();
        Result GetItemList(SalesListItemDto salesListItemDto);
        Result GetScheduleByTicketNumber(string ticketNumber);
        Result AddPayment(SalesPaymentDto salesPayment);
        Result AddListItem(SalesAddListItemDto salesAddListItem);
        Result DeleteTransactions(SalesItemDeleteDto salesItemDeleteDto);
        Result UpdateListItem(SalesUpdateItemDto salesUpdateItemDto);
        Result GetServicesWithPrice();
    }
}
