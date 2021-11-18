using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Checkout;
using Strive.BusinessEntities.DTO.Details;
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
            return ResultWrap(new CheckoutRal(_tenant).GetAllCheckoutDetails, checkoutDto, "GetCheckedInVehicleDetails");
        }
        public Result UpdateCheckoutDetails(CheckOutDto checkoutEntry)
        {
            return ResultWrap(new CheckoutRal(_tenant).UpdateCheckoutDetails, checkoutEntry, "SaveCheckoutTime");
        }
        public Result UpdateJobStatusHold(CheckoutHoldDto checkoutHoldDto)
        {
            var subject = EmailSubject.VehicleHold;
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            keyValues.Add("{{emailId}}", checkoutHoldDto.emailId);
            keyValues.Add("{{ticketNumber}}", checkoutHoldDto.TicketNumber);

            if (!string.IsNullOrEmpty(checkoutHoldDto.emailId))
                new CommonBpl(_cache, _tenant).SendEmail(HtmlTemplate.VehicleHold, checkoutHoldDto.emailId, keyValues, subject);

            return ResultWrap(new CheckoutRal(_tenant).UpdateJobStatusHold, checkoutHoldDto, "UpdateJobStatus");

        }
        public Result UpdateJobStatusComplete(JobStatusDto jobStatusDto)
        {
            return ResultWrap(new CheckoutRal(_tenant).UpdateJobStatusComplete, jobStatusDto, "UpdateJobStatus");
        }

        public Result GetCustomerHistory(CustomerHistorySearchDto salesReportDto)
        {
            return ResultWrap(new CheckoutRal(_tenant).GetCustomerHistory, salesReportDto, "CustomerHistory");
        }
    }
}
