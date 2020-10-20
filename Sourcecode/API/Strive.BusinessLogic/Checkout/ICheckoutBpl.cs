using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CheckoutEntry;
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
        Result GetCheckedInVehicleDetails();
        Result UpdateCheckoutDetails(CheckoutEntryDto checkoutEntry);
        Result UpdateJobStatus(HoldByJobIdDto holdByJobId);
    }
}
