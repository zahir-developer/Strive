using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.Sales;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Sales
{
    public class SalesBpl : Strivebase, ISalesBpl
    {
        public SalesBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        //public Result UpdateSalesItem(SalesDto sales)
        //{
        //    return ResultWrap(new SalesRal(_tenant).UpdateSalesItem, sales, "Status");
        //}
        public Result UpdateItem(SalesItemUpdateDto salesItemUpdateDto)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).UpdateItem,salesItemUpdateDto, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteItemById(int serviceId)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).DeleteItemById, serviceId, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public string GetTicketNumber()
        {
            var ticketNumberGenerator = new CommonBpl(_cache, _tenant).RandomNumber(6);
            return ticketNumberGenerator;
        }
    }
}
