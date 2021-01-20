using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CheckoutEntry;
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
        Result GetAllCheckoutDetails( int locationId);
        Result UpdateCheckoutDetails(CheckoutEntryDto checkoutEntry);
        Result UpdateJobStatusHold(JobIdDto jobIdDto);
        Result UpdateJobStatusComplete(JobIdDto jobIdDto);

        Result GetCustomerHistory(SalesReportDto salesReportDto);
    }
}
