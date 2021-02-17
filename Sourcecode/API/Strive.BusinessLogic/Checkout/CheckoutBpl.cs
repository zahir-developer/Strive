using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CheckoutEntry;
using Strive.BusinessEntities.DTO.Report;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Checkout
{
    public class CheckoutBpl : Strivebase, ICheckoutBpl
    {
        public CheckoutBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        public Result GetAllCheckoutDetails(SearchDto checkoutDto)
        {
            return ResultWrap(new CheckoutRal(_tenant).GetAllCheckoutDetails,checkoutDto, "GetCheckedInVehicleDetails");
        }
        public Result UpdateCheckoutDetails(CheckoutEntryDto checkoutEntry)
        {
            return ResultWrap(new CheckoutRal(_tenant).UpdateCheckoutDetails,checkoutEntry, "SaveCheckoutTime");
        }
        public Result UpdateJobStatusHold(CheckoutHoldDto checkoutHoldDto)
        {

            new CommonBpl(_cache, _tenant).SendHoldNotificationEmail(checkoutHoldDto.emailId, checkoutHoldDto.TicketNumber);


            return ResultWrap(new CheckoutRal(_tenant).UpdateJobStatusHold, checkoutHoldDto, "UpdateJobStatus");

            

        }
        public Result UpdateJobStatusComplete(JobIdDto jobIdDto)
        {
            return ResultWrap(new CheckoutRal(_tenant).UpdateJobStatusComplete, jobIdDto, "UpdateJobStatus");
        }

        public Result GetCustomerHistory (CustomerHistorySearchDto salesReportDto)
        {
            return ResultWrap(new CheckoutRal(_tenant).GetCustomerHistory, salesReportDto, "CustomerHistory");
        }
    }
}
