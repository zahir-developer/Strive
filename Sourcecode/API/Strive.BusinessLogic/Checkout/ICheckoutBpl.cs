using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Checkout;
using Strive.BusinessEntities.DTO.Details;
using Strive.BusinessEntities.DTO.Report;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Checkout
{
    public interface ICheckoutBpl
    {
        Result GetAllCheckoutDetails(SearchDto checkoutDto);
        Result UpdateCheckoutDetails(CheckOutDto checkoutEntry);
        Result UpdateJobStatusHold(CheckoutHoldDto checkoutHoldDto);
        Result UpdateJobStatusComplete(JobStatusDto jobStatusDto);

        Result GetCustomerHistory(CustomerHistorySearchDto salesReportDto);
    }
}
